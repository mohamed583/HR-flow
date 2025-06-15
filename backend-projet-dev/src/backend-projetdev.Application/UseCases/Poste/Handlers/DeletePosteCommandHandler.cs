using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class DeletePosteCommandHandler : IRequestHandler<DeletePosteCommand, Result>
    {
        private readonly IPosteRepository _posteRepository;

        public DeletePosteCommandHandler(IPosteRepository posteRepository)
        {
            _posteRepository = posteRepository;
        }

        public async Task<Result> Handle(DeletePosteCommand request, CancellationToken cancellationToken)
        {
            var poste = await _posteRepository.GetByIdAsync(request.Id);
            if (poste is null)
                return Result.Failure($"Aucun poste trouvé avec l'ID {request.Id}.");

            await _posteRepository.DeleteAsync(poste);
            return Result.SuccessResult("Poste supprimé avec succès.");
        }
    }
}
