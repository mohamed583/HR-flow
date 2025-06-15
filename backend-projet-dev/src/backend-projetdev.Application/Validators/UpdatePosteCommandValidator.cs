using backend_projetdev.Application.UseCases.Poste.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class UpdatePosteCommandValidator : AbstractValidator<UpdatePosteCommand>
    {
        public UpdatePosteCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Dto.Nom).NotEmpty();
            RuleFor(x => x.Dto.Description).NotEmpty();
        }
    }
}
