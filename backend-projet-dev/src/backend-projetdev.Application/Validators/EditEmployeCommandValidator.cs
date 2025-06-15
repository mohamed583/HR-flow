using backend_projetdev.Application.UseCases.Employe.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class EditEmployeCommandValidator : AbstractValidator<EditEmployeCommand>
    {
        public EditEmployeCommandValidator()
        {
            RuleFor(x => x.Data.Id).NotEmpty();
            RuleFor(x => x.Data.Nom).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Data.Prenom).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Data.Metier).MaximumLength(50);
            RuleFor(x => x.Data.EquipeId).GreaterThan(0);
            RuleFor(x => x.Data.Salaire).GreaterThan(0);
        }
    }
}
