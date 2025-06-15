using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using MediatR;

public class GetDetailsCandidatureHandler : IRequestHandler<GetDetailsCandidatureQuery, Result<CandidatureDto>>
{
    private readonly ICandidatureRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetDetailsCandidatureHandler(
        ICandidatureRepository repository,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Result<CandidatureDto>> Handle(GetDetailsCandidatureQuery request, CancellationToken cancellationToken)
    {
        var userId = await _currentUserService.GetUserIdAsync();
        var isAdmin = await _currentUserService.IsInRoleAsync("Admin");

        var candidature = await _repository.GetDetailsAsync(request.Id, userId, isAdmin);

        if (candidature is null)
            return Result<CandidatureDto>.Failure("Candidature non trouvée.");

        var dto = _mapper.Map<CandidatureDto>(candidature);
        return Result<CandidatureDto>.SuccessResult(dto);
    }
}
