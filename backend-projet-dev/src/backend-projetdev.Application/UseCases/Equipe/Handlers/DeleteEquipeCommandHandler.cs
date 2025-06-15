using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Equipe.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Equipe.Handlers
{
    public class DeleteEquipeCommandHandler : IRequestHandler<DeleteEquipeCommand, Result>
    {
        private readonly IEquipeRepository _equipeRepository;

        public DeleteEquipeCommandHandler(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<Result> Handle(DeleteEquipeCommand request, CancellationToken cancellationToken)
        {
            var equipe = await _equipeRepository.GetWithEmployesByIdAsync(request.Id);
            if (equipe == null)
                return Result.Failure("Équipe introuvable.");

            await _equipeRepository.DeleteAsync(equipe);
            return Result.SuccessResult("Équipe supprimée avec succès.");
        }
    }
}
