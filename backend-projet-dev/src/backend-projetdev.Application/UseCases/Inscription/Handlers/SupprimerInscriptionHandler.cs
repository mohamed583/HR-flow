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
    public class SupprimerInscriptionHandler : IRequestHandler<SupprimerInscriptionCommand, Result>
    {
        private readonly IInscriptionRepository _repo;

        public SupprimerInscriptionHandler(IInscriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(SupprimerInscriptionCommand request, CancellationToken cancellationToken)
        {
            return await _repo.SupprimerInscriptionAsync(request.InscriptionId, request.EmployeId);
        }
    }
}
