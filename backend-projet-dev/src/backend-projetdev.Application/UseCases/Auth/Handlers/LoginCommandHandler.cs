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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
    {
        private readonly ILoginService _loginService;

        public LoginCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _loginService.LoginAsync(request.Email, request.Password);
        }
    }
}
