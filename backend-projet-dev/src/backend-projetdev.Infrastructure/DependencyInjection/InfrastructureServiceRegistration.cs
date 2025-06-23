using backend_projetdev.Application.Interfaces;
using backend_projetdev.Infrastructure.Persistence;
using backend_projetdev.Infrastructure.Repositories;
using backend_projetdev.Infrastructure.Services;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend_projetdev.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<ICongeRepository, CongeRepository>();
            services.AddScoped<IEmployeRepository, EmployeRepository>();
            services.AddScoped<IEquipeRepository, EquipeRepository>();
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();
            services.AddScoped<IEntretienRepository, EntretienRepository>();
            services.AddScoped<IFormationRepository, FormationRepository>();
            services.AddScoped<IFormateurRepository, FormateurRepository>();
            services.AddScoped<IInscriptionRepository, InscriptionRepository>();
            services.AddScoped<IPaieRepository, PaieRepository>();
            services.AddScoped<IPosteRepository, PosteRepository>();
            services.AddScoped<ICandidatRepository, CandidatRepository>();
            services.AddScoped<ICandidatureRepository, CandidatureRepository>();
            services.AddScoped<IDepartementRepository, DepartementRepository>();

            // Services métiers
            services.AddScoped<IEmployeService, EmployeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPersonneService, PersonneService>();
            services.AddScoped<ICvMatchingService, CvMatchingService>();

            // Identity + HttpContext
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Authentication JWT
            var jwtSettings = configuration.GetSection("Jwt");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // à mettre à true en prod
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
                    ValidAudience = jwtSettings.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            return services;
        }
    }
}
