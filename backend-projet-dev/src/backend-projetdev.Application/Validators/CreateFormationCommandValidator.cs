using backend_projetdev.Application.UseCases.Formation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreateFormationCommandValidator : AbstractValidator<CreateFormationCommand>
    {
        public CreateFormationCommandValidator()
        {
            RuleFor(x => x.Titre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.DateDebut).NotEmpty();
            RuleFor(x => x.DateFin).NotEmpty()
                .GreaterThan(x => x.DateDebut).WithMessage("La date de fin doit être après la date de début.");
            RuleFor(x => x.FormateurId).NotEmpty();
            RuleFor(x => x.Cout).GreaterThanOrEqualTo(0);
        }
    }
}
