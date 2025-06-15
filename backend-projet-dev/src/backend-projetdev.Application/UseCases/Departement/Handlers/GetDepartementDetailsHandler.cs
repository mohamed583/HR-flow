using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Departement.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Departement.Handlers
{
    public class GetDepartementDetailsHandler : IRequestHandler<GetDepartementDetailsQuery, Result<DepartementDetailsDto>>
    {
        private readonly IDepartementRepository _repository;
        private readonly IMapper _mapper;
        public GetDepartementDetailsHandler(IDepartementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<DepartementDetailsDto>> Handle(GetDepartementDetailsQuery request, CancellationToken cancellationToken)
        {
            var departement = await _repository.GetByIdWithEquipesAsync(request.Id);
            if (departement == null)
                return Result<DepartementDetailsDto>.Failure("Département non trouvé.");
            var dto = _mapper.Map<DepartementDetailsDto>(departement);
            return Result<DepartementDetailsDto>.SuccessResult(dto);
        }
    }
}