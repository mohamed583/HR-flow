using backend_projetdev.Application.UseCases.Formateur.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreateFormateurCommandValidator : AbstractValidator<CreateFormateurCommand>
    {
        public CreateFormateurCommandValidator()
        {
            RuleFor(x => x.Formateur.Nom).NotEmpty();
            RuleFor(x => x.Formateur.Prenom).NotEmpty();
            RuleFor(x => x.Formateur.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Formateur.Password).MinimumLength(6);
            RuleFor(x => x.Formateur.Salaire).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Formateur.Domaine).NotEmpty();
        }
    }
}
