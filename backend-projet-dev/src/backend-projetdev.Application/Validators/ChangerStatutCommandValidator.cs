using backend_projetdev.Application.UseCases.Employe.Commands;
using backend_projetdev.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class ChangerStatutCommandValidator : AbstractValidator<ChangerStatutCommand>
    {
        public ChangerStatutCommandValidator()
        {
            RuleFor(x => x.Statut)
                .NotEmpty().WithMessage("Le nouveau statut est requis.");
        }

    }
}