using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Conge.Commands;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;
using MediatR;

public class CreateCongeCommandHandler : IRequestHandler<CreateCongeCommand, Result<CongeDto>>
{
    private readonly ICongeRepository _repository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmployeService _employeService;
    private readonly IMapper _mapper;

    public CreateCongeCommandHandler(
        ICongeRepository repository,
        ICurrentUserService currentUserService,
        IEmployeService employeService,
        IMapper mapper)
    {
        _repository = repository;
        _currentUserService = currentUserService;
        _employeService = employeService;
        _mapper = mapper;
    }

    public async Task<Result<CongeDto>> Handle(CreateCongeCommand request, CancellationToken cancellationToken)
    {
        var employeId = await _currentUserService.GetUserIdAsync();
        var employe = await _employeService.GetByIdAsync(employeId);
        if (employe == null)
            return Result<CongeDto>.Failure("Employé non trouvé.");
        
        var conge = _mapper.Map<Conge>(request.Dto);
        conge.EmployeId = employe.Id;
        conge.StatusConge = Status.EnCours;
        await _repository.AddAsync(conge);

        var dto = _mapper.Map<CongeDto>(conge);
        return Result<CongeDto>.SuccessResult(dto, "Congé créé avec succès.");

    }
}
