using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Mapping;
using backend_projetdev.Infrastructure.Persistence;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_projetdev.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICandidatRepository _candidatRepository;
        private static readonly Dictionary<string, string> _refreshTokens = new();
        private static readonly HashSet<string> _revokedAccessTokens = new();
        private readonly ApplicationDbContext _context;
        public LoginService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, ICandidatRepository candidatRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _candidatRepository = candidatRepository;
            _context = context;
        }

        public async Task<Result<TokenDto>> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Result<TokenDto>.Failure("Invalid login attempt");

            var accessToken = await GenerateJwtToken(user);
            var refreshToken = Guid.NewGuid().ToString();
            _refreshTokens[refreshToken] = user.Id;

            return Result<TokenDto>.SuccessResult(new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<Result<TokenDto>> RegisterAsync(string nom, string prenom, string email, string password)
        {
            var candidat = new Candidat
            {
                Id = Guid.NewGuid().ToString(),
                Nom = nom,
                Prenom = prenom,
                Email = email,
                UserName = email
            };

            var user = UserMapper.ToApplicationUser(candidat);
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return Result<TokenDto>.Failure("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Candidat");

            var accessToken = await GenerateJwtToken(user);
            var refreshToken = Guid.NewGuid().ToString();
            _refreshTokens[refreshToken] = user.Id;
            await _candidatRepository.AddAsync(candidat);
            return Result<TokenDto>.SuccessResult(new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<Result<TokenDto>> RefreshTokenAsync(string refreshToken)
        {
            if (!_refreshTokens.ContainsKey(refreshToken))
                return Result<TokenDto>.Failure("Invalid refresh token");

            var userId = _refreshTokens[refreshToken];
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result<TokenDto>.Failure("User not found");

            var newAccessToken = await GenerateJwtToken(user);
            var newRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            // Replace old token
            _refreshTokens.Remove(refreshToken);
            _refreshTokens[newRefreshToken] = user.Id;

            return Result<TokenDto>.SuccessResult(new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        public async Task<Result> LogoutAsync(string accessToken, string refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(refreshToken) && _refreshTokens.ContainsKey(refreshToken))
            {
                _refreshTokens.Remove(refreshToken);
            }

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _revokedAccessTokens.Add(accessToken);
            }

            return Result.SuccessResult();
        }
        public bool IsAccessTokenRevoked(string accessToken)
        {
            return _revokedAccessTokens.Contains(accessToken);
        }

        public async Task<Result> ChangeLoginAsync(string userId, string newEmail, string newUsername)
        {
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Result.Failure("User not found");

            user.Email = newEmail;
            user.UserName = newUsername;

            var result = await _userManager.UpdateAsync(user);
            
            if (!result.Succeeded)
                return Result.Failure("Login update failed " + string.Join(", ", result.Errors.Select(e => e.Description)));

            // Récupérer les rôles de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Employe"))
            {
                
                var employe = await _context.Employes.FindAsync(user.Id);
                if (employe != null)
                {
                    employe.Email = newEmail;
                    employe.UserName = newUsername;
                    _context.Employes.Update(employe);
                    await _context.SaveChangesAsync();
                }
            }
            else if (roles.Contains("Formateur"))
            {
                
                var formateur = await _context.Formateurs.FindAsync(user.Id);
                if (formateur != null)
                {
                    formateur.Email = newEmail;
                    formateur.UserName = newUsername;
                    _context.Formateurs.Update(formateur);
                    await _context.SaveChangesAsync();
                }

            }
            return result.Succeeded
                ? Result.SuccessResult()
                : Result.Failure("Login update failed " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
