using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Paie.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Paie.Handlers
{
    public class EffectuerPaiePourTousCommandHandler : IRequestHandler<EffectuerPaiePourTousCommand, Result<int>>
    {
        private readonly IPaieRepository _repository;
        private readonly IEmployeService _employeService;

        public EffectuerPaiePourTousCommandHandler(IPaieRepository repository, IEmployeService employeService)
        {
            _repository = repository;
            _employeService = employeService;
        }

        public async Task<Result<int>> Handle(EffectuerPaiePourTousCommand request, CancellationToken cancellationToken)
        {
            var employes = await _employeService.GetAllAsync();

            if (!employes.Any())
                return Result<int>.Failure("Aucun employé trouvé.");

            var paies = employes.Select(e => new Domain.Entities.Paie
            {
                PersonneId = e.Id,
                DatePaie = DateTime.Now,
                Montant = e.Salaire,
                Description = "Paie mensuelle",
                Avantages = "Aucun",
                Retenues = "Aucune"
            }).ToList();

            await _repository.AddRangeAsync(paies);
            return Result<int>.SuccessResult(paies.Count, "Paies générées pour tous les employés.");
        }
    }
}
