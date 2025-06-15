using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Commands;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class EditEmployeCommandHandler : IRequestHandler<EditEmployeCommand, Result>
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IEquipeRepository _equipeRepository;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public EditEmployeCommandHandler(
            IEmployeRepository employeRepository,
            IEquipeRepository equipeRepository,
            IRoleService roleService,
            IMapper mapper)
        {
            _employeRepository = employeRepository;
            _equipeRepository = equipeRepository;
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(EditEmployeCommand request, CancellationToken cancellationToken)
        {
            var employe = await _employeRepository.GetByIdAsync(request.Data.Id);
            if (employe == null)
                return Result.Failure("Employé non trouvé.");

            // Mapping
            _mapper.Map(request.Data, employe);
            await _employeRepository.UpdateAsync(employe);

            // Gestion des rôles
            if (request.Data.EstManager)
                await _roleService.AddUserToRoleAsync(employe.Id, "Manager");
            else
                await _roleService.RemoveUserFromRoleAsync(employe.Id, "Manager");

            return Result.SuccessResult("Employé modifié avec succès.");
        }
    }
}
