using backend_projetdev.Application.UseCases.Conge.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreateCongeCommandValidator : AbstractValidator<CreateCongeCommand>
    {
        public CreateCongeCommandValidator()
        {
            RuleFor(x => x.Dto.DateDebut)
                .NotEmpty().WithMessage("La date de début est obligatoire.");

            RuleFor(x => x.Dto.DateFin)
                .NotEmpty().WithMessage("La date de fin est obligatoire.")
                .GreaterThan(x => x.Dto.DateDebut).WithMessage("La date de fin doit être postérieure à la date de début.");

            RuleFor(x => x.Dto.Type)
                .NotEmpty().WithMessage("Le motif est obligatoire.");
        }
    }
}
