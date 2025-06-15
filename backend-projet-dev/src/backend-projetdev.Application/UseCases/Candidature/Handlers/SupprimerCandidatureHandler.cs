using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class SupprimerCandidatureHandler : IRequestHandler<SupprimerCandidatureCommand, Result>
    {
        private readonly ICandidatureRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        public SupprimerCandidatureHandler(ICandidatureRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(SupprimerCandidatureCommand request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();
            var result = await _repository.DeleteAsync(request.Id, userId);
            return result ? Result.SuccessResult("Candidature supprimée.") : Result.Failure("Candidature non trouvée.");
        }
    }
}
