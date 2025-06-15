using backend_projetdev.Application.UseCases.Paie.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreatePaieCommandValidator : AbstractValidator<CreatePaieCommand>
    {
        public CreatePaieCommandValidator()
        {
            RuleFor(x => x.PersonneId)
                .NotEmpty().WithMessage("L'identifiant de la personne est requis.");

            RuleFor(x => x.DatePaie)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La date de paie ne peut pas être dans le futur.");

            RuleFor(x => x.Montant)
                .GreaterThan(0).WithMessage("Le montant doit être supérieur à zéro.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La description est obligatoire.");

            RuleFor(x => x.Avantages)
                .NotNull().WithMessage("Les avantages doivent être spécifiés.");

            RuleFor(x => x.Retenues)
                .NotNull().WithMessage("Les retenues doivent être spécifiées.");
        }
    }
}
