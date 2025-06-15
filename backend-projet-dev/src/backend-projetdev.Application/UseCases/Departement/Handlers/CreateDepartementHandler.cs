using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Departement.Commands;
using backend_projetdev.Domain.Entities;
using MediatR;
namespace backend_projetdev.Application.UseCases.Departement.Handlers;
public class CreateDepartementHandler : IRequestHandler<CreateDepartementCommand, Result<DepartementDetailsDto>>
{
    private readonly IDepartementRepository _repository;
    private readonly IMapper _mapper; // AutoMapper

    public CreateDepartementHandler(IDepartementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DepartementDetailsDto>> Handle(CreateDepartementCommand request, CancellationToken cancellationToken)
    {
        var departement = new Domain.Entities.Departement { Nom = request.Dto.Nom };

        await _repository.AddAsync(departement);

        var dto = _mapper.Map<DepartementDetailsDto>(departement); // mapping

        return Result<DepartementDetailsDto>.SuccessResult(dto, "Département créé.");
    }
}
