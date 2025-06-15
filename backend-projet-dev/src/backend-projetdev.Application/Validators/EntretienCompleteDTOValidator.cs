using backend_projetdev.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Validators
{
    public class EntretienCompleteDTOValidator : AbstractValidator<EntretienCompleteDto>
    {
        public EntretienCompleteDTOValidator()
        {
            RuleFor(x => x.Commentaire)
                .NotEmpty().WithMessage("Le commentaire est requis.")
                .MaximumLength(10000).WithMessage("Le commentaire ne peut pas dépasser 10000 caractères.");
        }
    }
}