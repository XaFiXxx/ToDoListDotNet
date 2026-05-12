using FluentValidation;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(task => task.Title)
            .NotEmpty()
            .WithMessage("Le titre est obligatoire.");

        RuleFor(task => task.UserId)
            .GreaterThan(0)
            .WithMessage("L'utilisateur est obligatoire.");
    }
}