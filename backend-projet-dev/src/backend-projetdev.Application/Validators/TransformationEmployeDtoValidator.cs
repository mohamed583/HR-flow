using backend_projetdev.Application.DTOs;
using FluentValidation;

namespace backend_projetdev.Application.Validators
{
    public class TransformationEmployeDtoValidator : AbstractValidator<TransformationEmployeDto>
    {
        public TransformationEmployeDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("L'email est requis.")
                .EmailAddress().WithMessage("Format de l'email invalide.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Le mot de passe doit contenir au moins 6 caractères.");
            RuleFor(x => x.Metier).NotEmpty().WithMessage("Le métier est requis.");
            RuleFor(x => x.Salaire).GreaterThan(0).NotEmpty().WithMessage("Le Salaire est requis.");
            RuleFor(x => x.Contrat).NotEmpty().WithMessage("Le type de contrat est requis.");
        }
    }
}
