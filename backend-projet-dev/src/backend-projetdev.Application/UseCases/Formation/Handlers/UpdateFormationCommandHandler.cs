using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formation.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formation.Handlers
{
    public class UpdateFormationCommandHandler : IRequestHandler<UpdateFormationCommand, Result>
    {
        private readonly IFormationRepository _formationRepository;
        private readonly IFormateurRepository _formateurRepository;

        public UpdateFormationCommandHandler(
            IFormationRepository formationRepository,
            IFormateurRepository formateurRepository)
        {
            _formationRepository = formationRepository;
            _formateurRepository = formateurRepository;
        }

        public async Task<Result> Handle(UpdateFormationCommand request, CancellationToken cancellationToken)
        {
            var formation = await _formationRepository.GetByIdAsync(request.Id);
            if (formation == null)
                return Result.Failure("Formation non trouvée.");

            var formateur = await _formateurRepository.GetByIdAsync(request.FormateurId);
            if (formateur == null)
                return Result.Failure("Formateur introuvable.");

            formation.Titre = request.Titre;
            formation.Description = request.Description;
            formation.DateDebut = request.DateDebut;
            formation.DateFin = request.DateFin;
            formation.FormateurId = request.FormateurId;
            formation.Cout = request.Cout;

            await _formationRepository.UpdateAsync(formation);
            return Result.SuccessResult();
        }
    }

}
