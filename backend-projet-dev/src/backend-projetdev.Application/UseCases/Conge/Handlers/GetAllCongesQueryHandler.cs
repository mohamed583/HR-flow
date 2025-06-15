using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Conge.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Conge.Handlers
{
    public class GetAllCongesQueryHandler : IRequestHandler<GetAllCongesQuery, Result<List<CongeDto>>>
    {
        private readonly ICongeRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeService _employeService;
        private readonly IMapper _mapper;

        public GetAllCongesQueryHandler(
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

        public async Task<Result<List<CongeDto>>> Handle(GetAllCongesQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _currentUserService.GetUserIdAsync();
            var employe = await _employeService.GetByIdAsync(currentUserId);

            if (employe == null)
                return Result<List<CongeDto>>.Failure("Employé non trouvé.");

            var isAdmin = await _currentUserService.IsInRoleAsync("Admin");
            var isManager = await _currentUserService.IsInRoleAsync("Manager");

            if (!isAdmin && !isManager)
                return Result<List<CongeDto>>.Failure("Accès refusé.");

            var conges = isAdmin
                ? await _repository.GetAllAsync()
                : await _repository.GetByEquipeIdAsync(employe.EquipeId);

            var dto = _mapper.Map<List<CongeDto>>(conges);
            return Result<List<CongeDto>>.SuccessResult(dto);
        }
    }
}