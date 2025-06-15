using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class DeleteEmployeCommandHandler : IRequestHandler<DeleteEmployeCommand, Result>
    {
        private readonly IEmployeRepository _repository;

        public DeleteEmployeCommandHandler(IEmployeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteEmployeCommand request, CancellationToken cancellationToken)
        {
            var employe = await _repository.GetByIdAsync(request.Id);
            if (employe == null)
                return Result.Failure("Employé non trouvé.");

            await _repository.DeleteAsync(employe);
            return Result.SuccessResult("Employé supprimé avec succès.");
        }
    }
}