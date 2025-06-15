using backend_projetdev.Application.UseCases.Poste.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class CreatePosteCommandValidator : AbstractValidator<CreatePosteCommand>
    {
        public CreatePosteCommandValidator()
        {
            RuleFor(x => x.Information.Nom).NotEmpty();
            RuleFor(x => x.Information.Description).NotEmpty();
        }
    }

}
