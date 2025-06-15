using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<TokenDto>>
    {
        private readonly ILoginService _loginService;

        public RegisterCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result<TokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _loginService.RegisterAsync(
                request.Nom,
                request.Prenom,
                request.Email,
                request.Password
            );
        }
    }
}
