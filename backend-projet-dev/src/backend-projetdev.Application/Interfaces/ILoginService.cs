using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface ILoginService
    {
        Task<Result> ChangeLoginAsync(string userId, string newEmail, string newUsername);
        Task<bool> ChangePasswordAsync(string userId, string newPassword);
        bool IsAccessTokenRevoked(string accessToken);
        Task<Result<TokenDto>> LoginAsync(string email, string password);
        Task<Result<TokenDto>> RegisterAsync(string nom, string prenom, string email, string password);
        Task<Result<TokenDto>> RefreshTokenAsync(string refreshToken);
        Task<Result> LogoutAsync(string accessToken, string refreshToken);
    }

}
