
using backend_projetdev.Application.UseCases.Auth.Commands;
using FluentValidation;




namespace backend_projetdev.Application.Validators.Auth
{
    public class ChangeLoginInfoValidator : AbstractValidator<ChangeLoginInfoCommand>
    {
        public ChangeLoginInfoValidator()
        {
            RuleFor(x => x.Data.NewEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.Data.NewPassword).NotEmpty().MinimumLength(6);
        }
    }
}

