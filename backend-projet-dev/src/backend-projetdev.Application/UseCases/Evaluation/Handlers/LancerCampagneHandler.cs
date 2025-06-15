using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Evaluation.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Handlers
{
    public class LancerCampagneHandler : IRequestHandler<LancerCampagneCommand, Result>
    {
        private readonly IEvaluationRepository _repository;
        private readonly IEmployeRepository _employeRepository;
        private readonly ICurrentUserService _currentUserService;

        public LancerCampagneHandler(
            IEvaluationRepository repository,
            IEmployeRepository employeRepository,
            ICurrentUserService currentUserService)
        {
            _repository = repository;
            _employeRepository = employeRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(LancerCampagneCommand request, CancellationToken cancellationToken)
        {
            var admin = await _employeRepository.GetAdminAsync();
            if (admin == null)
                return Result.Failure("Aucun administrateur trouvé.");

            var employes = await _employeRepository.GetAllAsync();

            foreach (var employe in employes)
            {
                string responsableId;

                if (await _employeRepository.HasRoleAsync(employe.Id, "Admin"))
                {
                    // Un admin est son propre responsable
                    responsableId = employe.Id;
                }
                else if (await _employeRepository.HasRoleAsync(employe.Id, "Manager"))
                {
                    // Un manager est évalué par l'admin
                    responsableId = admin.Id;
                }
                else
                {
                    // Employé standard → évalué par son manager
                    var manager = await _employeRepository.GetManagerByEquipeIdAsync(employe.EquipeId);
                    if (manager == null)
                        return Result.Failure($"Aucun manager trouvé pour le département de l'employé {employe.Nom}.");
                    responsableId = manager.Id;
                }

                var evaluation = new Domain.Entities.Evaluation
                {
                    EmployeId = employe.Id,
                    ResponsableId = responsableId,
                    Description = "compagne d'evaluation",
                    Objectifs = "evaluations des objectifs semestriels",
                    CommentairesEmploye = "",
                    CommentairesResponsable = "",
                    Note = 0,
                    FinaliseParEmploye = false,
                    FinaliseParManager = false,
                    EstApprouve = EstApprouve.EnCours,
                    DateEvaluation = DateTime.Now
                };

                await _repository.AddAsync(evaluation);
            }

            return Result.SuccessResult();
        }

    }

}
