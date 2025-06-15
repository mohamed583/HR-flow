using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using AutoMapper;
using MediatR;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.Common;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class GetCandidaturesParPosteHandler : IRequestHandler<GetCandidaturesParPosteQuery, Result<List<CandidatureDto>>>
    {
        private readonly ICandidatureRepository _repository;
        private readonly IMapper _mapper;

        public GetCandidaturesParPosteHandler(ICandidatureRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<CandidatureDto>>> Handle(GetCandidaturesParPosteQuery request, CancellationToken cancellationToken)
        {
            var candidatures = await _repository.GetByPosteIdAsync(request.PosteId);

            if (candidatures == null || !candidatures.Any())
                return Result<List<CandidatureDto>>.Failure("Aucune candidature trouvée pour ce poste.");

            var dtoList = _mapper.Map<List<CandidatureDto>>(candidatures);
            return Result<List<CandidatureDto>>.SuccessResult(dtoList);
        }
    }
}
