
namespace backend_projetdev.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string> GetUserIdAsync();
        Task<bool> IsInRoleAsync(string role);
    }
}
