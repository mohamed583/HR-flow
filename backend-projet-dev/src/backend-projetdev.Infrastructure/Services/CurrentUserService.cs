using backend_projetdev.Application.Interfaces;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend_projetdev.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<string> GetUserIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        public async Task<bool> IsInRoleAsync(string role)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return false;

            var appUser = await _userManager.FindByIdAsync(userId);
            if (appUser == null) return false;

            return await _userManager.IsInRoleAsync(appUser, role);
        }
    }
}
