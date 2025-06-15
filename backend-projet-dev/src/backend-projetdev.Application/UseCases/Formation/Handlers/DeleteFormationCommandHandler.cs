using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formation.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formation.Handlers
{
    public class DeleteFormationCommandHandler : IRequestHandler<DeleteFormationCommand, Result>
    {
        private readonly IFormationRepository _formationRepository;

        public DeleteFormationCommandHandler(IFormationRepository formationRepository)
        {
            _formationRepository = formationRepository;
        }

        public async Task<Result> Handle(DeleteFormationCommand request, CancellationToken cancellationToken)
        {
            var formation = await _formationRepository.GetByIdAsync(request.Id);
            if (formation == null)
                return Result.Failure("Formation non trouvée.");

            await _formationRepository.DeleteAsync(formation);
            return Result.SuccessResult();
        }
    }

}
