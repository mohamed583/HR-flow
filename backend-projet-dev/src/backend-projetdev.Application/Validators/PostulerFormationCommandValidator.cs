using backend_projetdev.Application.UseCases.Inscription.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class PostulerFormationCommandValidator : AbstractValidator<PostulerFormationCommand>
    {
        public PostulerFormationCommandValidator()
        {
            RuleFor(x => x.FormationId).GreaterThan(0).WithMessage("L'ID de la formation doit être valide.");
            RuleFor(x => x.EmployeId).NotEmpty().WithMessage("L'identifiant de l'employé est requis.");
        }
    }
}