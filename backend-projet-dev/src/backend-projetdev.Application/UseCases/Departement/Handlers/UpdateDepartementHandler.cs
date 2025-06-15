using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Departement.Commands;
using MediatR;

namespace backend_projetdev.Application.UseCases.Departement.Handlers
{
    public class UpdateDepartementHandler : IRequestHandler<UpdateDepartementCommand, Result<DepartementDetailsDto>>
    {
        private readonly IDepartementRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDepartementHandler(IDepartementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<DepartementDetailsDto>> Handle(UpdateDepartementCommand request, CancellationToken cancellationToken)
        {
            var departement = await _repository.GetByIdAsync(request.Id);
            if (departement == null)
                return Result<DepartementDetailsDto>.Failure("Département non trouvé.");

            departement.Nom = request.Dto.Nom;
            await _repository.UpdateAsync(departement);

            var dto = _mapper.Map<DepartementDetailsDto>(departement);
            return Result<DepartementDetailsDto>.SuccessResult(dto, "Département mis à jour.");
        }
    }

}