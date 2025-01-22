using FluentValidation;

namespace SourceSafe.Application.Services.UserServices.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(20);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(p => p.Password).NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(16)
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.\;\@\$\#\(\)\^\%\&\-\`]+").WithMessage("Your password must contain at least one NonAlphanumeric character");
    }
}
