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
    public class ApproveInscriptionHandler : IRequestHandler<ApproveInscriptionCommand, Result>
    {
        private readonly IInscriptionRepository _repo;

        public ApproveInscriptionHandler(IInscriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(ApproveInscriptionCommand request, CancellationToken cancellationToken)
        {
            var success = await _repo.ApproveInscriptionAsync(request.InscriptionId);
            return success ? Result.SuccessResult("Inscription approuvée.") : Result.Failure("Inscription non trouvée.");
        }
    }
}
