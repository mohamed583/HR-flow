using backend_projetdev.Application.UseCases.Conge.Commands.YourProject.Application.UseCases.Conge.Commands.UpdateStatusConge;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class UpdateStatusCongeCommandValidator : AbstractValidator<UpdateStatusCongeCommand>
    {
        public UpdateStatusCongeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("L'identifiant du congé est invalide.");

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Le statut du congé est invalide.");
        }
    }
}