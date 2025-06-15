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
    public class ApprouverEvaluationHandler : IRequestHandler<ApprouverEvaluationCommand, Result>
    {
        private readonly IEvaluationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public ApprouverEvaluationHandler(IEvaluationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(ApprouverEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _repository.GetByIdAsync(request.EvaluationId);
            if (evaluation == null)
                return Result.Failure("Évaluation non trouvée.");

            var userId = await _currentUserService.GetUserIdAsync();
            if (evaluation.EmployeId != userId)
                return Result.Failure("Non autorisé.");

            evaluation.EstApprouve = EstApprouve.Oui;
            await _repository.UpdateAsync(evaluation);

            return Result.SuccessResult();
        }
    }

}
