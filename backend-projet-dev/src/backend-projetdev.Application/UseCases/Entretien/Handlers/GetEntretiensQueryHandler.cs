using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Entretien.Queries;
using backend_projetdev.Domain.Entities;
using MediatR;

namespace backend_projetdev.Application.UseCases.Entretien.Handlers
{
    public class GetEntretiensQueryHandler : IRequestHandler<GetEntretiensQuery, Result<List<EntretienDto>>>
    {
        private readonly IEntretienRepository _repository;
        private readonly IMapper _mapper;
        public GetEntretiensQueryHandler(IEntretienRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<EntretienDto>>> Handle(GetEntretiensQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();
            var dto = _mapper.Map<List<EntretienDto>>(result);
            return Result<List<EntretienDto>>.SuccessResult(dto);
        }
    }
}