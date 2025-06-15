using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Evaluation.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Handlers
{
    public class DeleteEvaluationHandler : IRequestHandler<DeleteEvaluationCommand, Result>
    {
        private readonly IEvaluationRepository _repository;

        public DeleteEvaluationHandler(IEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _repository.GetByIdAsync(request.EvaluationId);
            if (evaluation == null)
                return Result.Failure("Évaluation non trouvée.");

            await _repository.DeleteAsync(evaluation);
            return Result.SuccessResult();
        }
    }

}
