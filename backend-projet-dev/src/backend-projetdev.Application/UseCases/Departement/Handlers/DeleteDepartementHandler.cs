using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Departement.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Departement.Handlers
{
    public class DeleteDepartementHandler : IRequestHandler<DeleteDepartementCommand, Result>
    {
        private readonly IDepartementRepository _repository;

        public DeleteDepartementHandler(IDepartementRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteDepartementCommand request, CancellationToken cancellationToken)
        {
            var departement = await _repository.GetByIdWithEquipesAsync(request.Id);
            if (departement == null)
                return Result.Failure("Département non trouvé.");

            if (departement.Equipes.Any())
                return Result.Failure("Impossible de supprimer un département qui contient des équipes.");

            await _repository.DeleteAsync(departement);
            return Result.SuccessResult("Département supprimé.");
        }
    }
}