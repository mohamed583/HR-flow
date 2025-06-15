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
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
    {
        private readonly ILoginService _loginService;

        public RefreshTokenCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _loginService.RefreshTokenAsync(request.RefreshToken);
        }
    }
}