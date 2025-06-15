using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Inscription.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Inscription.Handlers
{
    public class RejectInscriptionHandler : IRequestHandler<RejectInscriptionCommand, Result>
    {
        private readonly IInscriptionRepository _repo;

        public RejectInscriptionHandler(IInscriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(RejectInscriptionCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.RejectInscriptionAsync(request.InscriptionId);
            return success ? Result.SuccessResult("Inscription rejetée.") : Result.Failure("Inscription non trouvée.");
        }
    }
}
