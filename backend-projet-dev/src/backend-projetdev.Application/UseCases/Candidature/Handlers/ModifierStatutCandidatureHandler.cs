using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Commands;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class ModifierStatutCandidatureHandler : IRequestHandler<ModifierStatutCandidatureCommand, Result>
    {
        private readonly ICandidatureRepository _repository;

        public ModifierStatutCandidatureHandler(ICandidatureRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ModifierStatutCandidatureCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.UpdateStatusAsync(request.Id, request.Status);
            return result ? Result.SuccessResult("Statut modifié.") : Result.Failure("Erreur lors de la modification.");
        }
    }


}
