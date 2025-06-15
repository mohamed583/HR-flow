using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Entretien.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Handlers
{
    public class CompleteEntretienCommandHandler : IRequestHandler<CompleteEntretienCommand, Result>
    {
        private readonly IEntretienRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public CompleteEntretienCommandHandler(IEntretienRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(CompleteEntretienCommand request, CancellationToken cancellationToken)
        {
            var employeId = await _currentUserService.GetUserIdAsync();

            var entretien = await _repository.GetNonFinalisedByIdAndEmployeIdAsync(request.EntretienId, employeId);
            if (entretien == null)
                return Result.Failure("Entretien introuvable ou déjà finalisé.");

            entretien.Status = StatusEntretien.Finalise;
            entretien.Commentaire = request.Model.Commentaire;

            await _repository.UpdateAsync(entretien);
            return Result.SuccessResult("Entretien finalisé avec succès.");
        }
    }

}
