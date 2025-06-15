using backend_projetdev.Application.UseCases.Employe.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class DeleteEmployeCommandValidator : AbstractValidator<DeleteEmployeCommand>
    {
        public DeleteEmployeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
