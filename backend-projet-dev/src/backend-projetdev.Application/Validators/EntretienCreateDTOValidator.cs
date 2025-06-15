using backend_projetdev.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class EntretienCreateDTOValidator : AbstractValidator<EntretienCreateDto>
    {
        public EntretienCreateDTOValidator()
        {
            RuleFor(x => x.CandidatureId).NotEmpty().WithMessage("CandidatureId est requis.");
            RuleFor(x => x.EmployeId).NotEmpty().WithMessage("EmployeId est requis.");
            RuleFor(x => x.DateEntretien).NotEmpty().WithMessage("La date d’entretien est requise.");
        }
    }
}