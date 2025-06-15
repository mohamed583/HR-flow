using backend_projetdev.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace backend_projetdev.API.Filters
{
    public class TokenRevocationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public TokenRevocationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var loginService = scope.ServiceProvider.GetRequiredService<ILoginService>();

                var authHeader = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader["Bearer ".Length..].Trim();

                    if (loginService.IsAccessTokenRevoked(token))
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Token has been revoked");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
