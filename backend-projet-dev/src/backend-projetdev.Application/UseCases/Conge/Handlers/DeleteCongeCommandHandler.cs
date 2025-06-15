using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Conge.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Conge.Handlers
{
    public class DeleteCongeCommandHandler : IRequestHandler<DeleteCongeCommand, Result>
    {
        private readonly ICongeRepository _congeRepository;

        public DeleteCongeCommandHandler(ICongeRepository congeRepository)
        {
            _congeRepository = congeRepository;
        }

        public async Task<Result> Handle(DeleteCongeCommand request, CancellationToken cancellationToken)
        {
            var conge = await _congeRepository.GetByIdAsync(request.Id);

            if (conge == null || conge.StatusConge != Status.EnCours)
                return Result.Failure("Congé introuvable ou ne peut pas être supprimé.");

            await _congeRepository.DeleteAsync(conge);
            return Result.SuccessResult("Congé supprimé avec succès.");
        }
    }
}
