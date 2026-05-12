using FluentValidation;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(task => task.Title)
            .NotEmpty()
            .WithMessage("Le titre est obligatoire.");
    }
}