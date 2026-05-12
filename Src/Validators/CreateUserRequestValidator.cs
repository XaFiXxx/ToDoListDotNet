using FluentValidation;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("Le pseudo est obligatoire.");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("L'email est obligatoire.")
            .EmailAddress()
            .WithMessage("L'email n'est pas valide.");

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Le mot de passe est obligatoire.")
            .MinimumLength(6)
            .WithMessage("Le mot de passe doit contenir au moins 6 caractères.");
    }
}