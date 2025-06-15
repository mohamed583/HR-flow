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
    public class UpdateEquipeCommandHandler : IRequestHandler<UpdateEquipeCommand, Result>
    {
        private readonly IEquipeRepository _equipeRepository;

        public UpdateEquipeCommandHandler(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<Result> Handle(UpdateEquipeCommand request, CancellationToken cancellationToken)
        {
            var equipe = await _equipeRepository.GetByIdAsync(request.Id);
            if (equipe == null)
                return Result.Failure("Équipe introuvable.");

            equipe.Nom = request.Nom;
            equipe.DepartementId = request.DepartementId;

            await _equipeRepository.UpdateAsync(equipe);
            return Result.SuccessResult("Équipe mise à jour avec succès.");
        }
    }
}
