using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Departement.Queries;
using backend_projetdev.Domain.Entities;
using MediatR;

namespace backend_projetdev.Application.UseCases.Departement.Handlers
{
    public class GetDepartementsHandler : IRequestHandler<GetDepartementsQuery, Result<List<DepartementDetailsDto>>>
    {
        private readonly IDepartementRepository _repository;
        private readonly IMapper _mapper;
        public GetDepartementsHandler(IDepartementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<DepartementDetailsDto>>> Handle(GetDepartementsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllWithEquipesAsync();
            var dto = _mapper.Map<List<DepartementDetailsDto>>(result);
            return Result<List<DepartementDetailsDto>>.SuccessResult(dto);
        }
    }
}