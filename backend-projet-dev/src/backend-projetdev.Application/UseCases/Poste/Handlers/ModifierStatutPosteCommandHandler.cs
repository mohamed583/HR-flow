using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class ModifierStatutPosteCommandHandler : IRequestHandler<ModifierStatutPosteCommand, Result>
    {
        private readonly IPosteRepository _posteRepository;

        public ModifierStatutPosteCommandHandler(IPosteRepository posteRepository)
        {
            _posteRepository = posteRepository;
        }

        public async Task<Result> Handle(ModifierStatutPosteCommand request, CancellationToken cancellationToken)
        {
            var poste = await _posteRepository.GetByIdAsync(request.Id);
            if (poste is null)
                return Result.Failure($"Aucun poste trouvé avec l'ID {request.Id}.");

            poste.StatutPoste = request.NouveauStatut;
            await _posteRepository.UpdateAsync(poste);
            return Result.SuccessResult("Statut du poste modifié avec succès.");
        }
    }
}
