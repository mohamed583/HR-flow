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
    public class ChangeLoginInfoCommandHandler : IRequestHandler<ChangeLoginInfoCommand, Result>
    {
        private readonly ILoginService _loginService;

        public ChangeLoginInfoCommandHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result> Handle(ChangeLoginInfoCommand request, CancellationToken cancellationToken)
        {
            var result = await _loginService.ChangeLoginAsync(request.Id, request.Data.NewEmail, request.Data.NewEmail);
            if (!result.Success)
                return result;

            if (!string.IsNullOrEmpty(request.Data.NewPassword))
            {
                var passwordResult = await _loginService.ChangePasswordAsync(request.Id, request.Data.NewPassword);
                if (!passwordResult)
                    return Result.Failure("Erreur lors du changement de mot de passe.");
            }

            return Result.SuccessResult("Informations de connexion mises à jour avec succès.");
        }
    }
}
