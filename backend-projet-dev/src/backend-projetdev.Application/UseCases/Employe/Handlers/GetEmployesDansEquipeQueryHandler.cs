using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class GetEmployesDansEquipeQueryHandler : IRequestHandler<GetEmployesDansEquipeQuery, Result<List<EmployeDto>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeRepository _employeRepository;
        private readonly IMapper _mapper;

        public GetEmployesDansEquipeQueryHandler(
            ICurrentUserService currentUserService,
            IEmployeRepository employeRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _employeRepository = employeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeDto>>> Handle(GetEmployesDansEquipeQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();
            if (string.IsNullOrEmpty(userId))
                return Result<List<EmployeDto>>.Failure("Utilisateur non authentifié.");

            var employeConnecte = await _employeRepository.GetByIdWithEquipeAsync(userId);
            if (employeConnecte == null)
                return Result<List<EmployeDto>>.Failure("Employé non trouvé.");

            if (employeConnecte.EquipeId == null)
                return Result<List<EmployeDto>>.Failure("Aucune équipe associée à cet employé.");

            var employesEquipe = await _employeRepository.GetByEquipeIdAsync(employeConnecte.EquipeId);
            var dtoList = _mapper.Map<List<EmployeDto>>(employesEquipe);

            return Result<List<EmployeDto>>.SuccessResult(dtoList);
        }
    }
}
