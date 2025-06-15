using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class ChangerStatutCommandHandler : IRequestHandler<ChangerStatutCommand, Result>
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly ICurrentUserService _currentUserService;

        public ChangerStatutCommandHandler(IEmployeRepository employeRepository, ICurrentUserService currentUserService)
        {
            _employeRepository = employeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ChangerStatutCommand request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();

            var employe = await _employeRepository.GetByIdAsync(userId);
            if (employe == null)
                return Result.Failure("Employé non trouvé.");

            employe.Statut = request.Statut;
            await _employeRepository.UpdateAsync(employe);

            return Result.SuccessResult("Statut mis à jour avec succès.");
        }
    }
}
