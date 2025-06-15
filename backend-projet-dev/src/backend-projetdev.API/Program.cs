using backend_projetdev.API.Extensions;
using backend_projetdev.API.Filters;
using backend_projetdev.Application.DependencyInjection;
using backend_projetdev.Infrastructure.DependencyInjection;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Déclaration du nom de la politique CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Ajout de la politique CORS pour autoriser ton frontend React
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // URL frontend React
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
builder.Services.AddHttpContextAccessor();
// 1. Add layered services
builder.Services.AddApplicationServices(); // Application layer (MediatR, AutoMapper, FluentValidation)
builder.Services.AddInfrastructure(builder.Configuration); // Infrastructure (EF, Identity, Repos, JWT)
builder.Services.AddPresentation(builder.Configuration); // Presentation (Swagger, Controllers, etc.)

var app = builder.Build();

// Applique automatiquement les migrations au démarrage
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// 2. Middleware pipeline
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<TokenRevocationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR flow API V1");
    c.RoutePrefix = string.Empty; // Accès direct à la racine
});

// 3. Init Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Candidat", "Formateur", "Employe", "Manager", "Admin" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 4. Map Controllers
app.MapControllers();

app.Run();
