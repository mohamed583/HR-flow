using backend_projetdev.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace backend_projetdev.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePresentation(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR flow API v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }
    }
}
