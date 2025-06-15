using backend_projetdev.Application.UseCases.Evaluation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreateEvaluationCommandValidator : AbstractValidator<CreateEvaluationCommand>
    {
        public CreateEvaluationCommandValidator()
        {
            RuleFor(x => x.DateEvaluation)
                .NotEmpty().WithMessage("La date est requise.");

            RuleFor(x => x.EmployeId)
                .NotEmpty().WithMessage("L'identifiant de l'employé est requis.");
        }
    }
}