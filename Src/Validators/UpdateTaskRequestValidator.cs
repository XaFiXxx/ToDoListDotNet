using FluentValidation;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(task => task.Title)
            .NotEmpty()
            .WithMessage("Le titre est obligatoire.");
    }
}