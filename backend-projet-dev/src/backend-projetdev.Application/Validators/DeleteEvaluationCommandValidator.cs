using backend_projetdev.Application.UseCases.Evaluation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class DeleteEvaluationCommandValidator : AbstractValidator<DeleteEvaluationCommand>
    {
        public DeleteEvaluationCommandValidator()
        {
            RuleFor(x => x.EvaluationId)
                .GreaterThan(0).WithMessage("L'identifiant est requis.");
        }
    }
}
