using AutoMapper;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class GetMesCandidaturesHandler : IRequestHandler<GetMesCandidaturesQuery, Result<List<CandidatureDto>>>
    {
        private readonly ICandidatureRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMesCandidaturesHandler(
            ICandidatureRepository repository,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<CandidatureDto>>> Handle(GetMesCandidaturesQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();

            if (string.IsNullOrEmpty(userId))
                return Result<List<CandidatureDto>>.Failure("Utilisateur non authentifié.");

            var candidatures = await _repository.GetByCandidatIdAsync(userId);

            if (candidatures == null || !candidatures.Any())
                return Result<List<CandidatureDto>>.SuccessResult(null, "Aucune candidature trouvée.");

            var dtoList = _mapper.Map<List<CandidatureDto>>(candidatures);
            return Result<List<CandidatureDto>>.SuccessResult(dtoList);
        }
    }
}
