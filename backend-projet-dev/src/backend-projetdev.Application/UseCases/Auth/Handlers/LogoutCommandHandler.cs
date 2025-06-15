using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Auth.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Auth.Handlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private readonly ILoginService _loginService;

        public LogoutCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // Ici on ne reçoit pas l'accessToken, il sera injecté dans le controller
            // On laisse le controller envoyer RefreshToken et AccessToken à LogoutAsync
            return await _loginService.LogoutAsync(null, request.RefreshToken);
        }
    }
}