using backend_projetdev.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class ManageEquipeDTOValidator : AbstractValidator<ManageEquipeDto>
    {
        public ManageEquipeDTOValidator()
        {
            RuleFor(x => x.Nom).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DepartementId).GreaterThan(0);
        }
    }

}
