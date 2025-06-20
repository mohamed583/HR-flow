using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Conge.Commands.YourProject.Application.UseCases.Conge.Commands.UpdateStatusConge;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Conge.Handlers
{
    public class UpdateStatusCongeCommandHandler : IRequestHandler<UpdateStatusCongeCommand, Result>
    {
        private readonly ICongeRepository _repository;
        private readonly IEmployeService _employeService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateStatusCongeCommandHandler(
            ICongeRepository repository,
            IEmployeService employeService,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _employeService = employeService;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(UpdateStatusCongeCommand request, CancellationToken cancellationToken)
        {
            var conge = await _repository.GetByIdAsync(request.Id);
            if (conge == null)
                return Result.Failure("Congé non trouvé.");

            var currentUserId = await _currentUserService.GetUserIdAsync();
            var employe = await _employeService.GetByIdAsync(currentUserId);

            if (employe == null)
                return Result.Failure("Employé non trouvé.");

            var isAdmin = await _currentUserService.IsInRoleAsync("Admin");
            var isManager = await _currentUserService.IsInRoleAsync("Manager");

            if (!isAdmin && !isManager)
                return Result.Failure("Accès refusé.");

            conge.StatusConge = request.NewStatus;
            await _repository.UpdateAsync(conge);

            return Result.SuccessResult("Statut du congé mis à jour.");
        }
    }
}