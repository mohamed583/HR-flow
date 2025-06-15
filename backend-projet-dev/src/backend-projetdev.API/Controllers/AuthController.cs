using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Auth.Commands;
using backend_projetdev.Application.UseCases.Auth.Queries;
using backend_projetdev.Application.UseCases.Departement.Queries;
using backend_projetdev.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoginService _loginService;

        public AuthController(IMediator mediator, ILoginService loginService)
        {
            _mediator = mediator;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(new { Token = result.Data }) : Unauthorized(new { result.Message });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(new { Token = result.Data }) : BadRequest(new { result.Message });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            // Récupérer le token d'accès depuis le header Authorization
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized(new { message = "Access token is missing or invalid" });

            var accessToken = authHeader["Bearer ".Length..].Trim();

            // Appeler directement la méthode du service pour passer l'access token
            var result = await _loginService.LogoutAsync(accessToken, command.RefreshToken);

            return result.Success
                ? Ok(new { message = "Logged out successfully" })
                : BadRequest(new { result.Message });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success
                ? Ok(result.Data)
                : Unauthorized(new { result.Message });
        }
        
        [HttpPut("ChangeLoginInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeLoginInfo([FromBody] ChangeLoginInfoCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var result = await _mediator.Send(new GetMeQuery());
            return result.Success ? Ok(result.Data) : NotFound(new { result.Message });
        }

    }
}