using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formateur.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formateur.Handlers
{
    public class DeleteFormateurCommandHandler : IRequestHandler<DeleteFormateurCommand, Result>
    {
        private readonly IFormateurRepository _repository;

        public DeleteFormateurCommandHandler(IFormateurRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteFormateurCommand request, CancellationToken cancellationToken)
        {
            var formateur = await _repository.GetByIdAsync(request.Id);
            if (formateur == null)
                return Result.Failure("Formateur non trouvé.");

            var success = await _repository.DeleteAsync(formateur);
            if (!success)
                return Result.Failure("Échec de la suppression du formateur.");

            return Result.SuccessResult("Formateur supprimé avec succès.");
        }
    }
}
