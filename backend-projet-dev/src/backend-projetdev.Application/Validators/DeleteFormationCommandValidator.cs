using backend_projetdev.Application.UseCases.Formation.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class DeleteFormationCommandValidator : AbstractValidator<DeleteFormationCommand>
    {
        public DeleteFormationCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

}
