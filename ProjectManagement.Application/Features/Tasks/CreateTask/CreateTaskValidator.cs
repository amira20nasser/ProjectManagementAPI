using FluentValidation;

namespace ProjectManagement.Application.Features.Tasks.CreateTask
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId is required.");

            RuleFor(x => x.Priority)
                .NotEmpty()
                .IsEnumName(typeof(ProjectManagement.Domain.Enums.TaskPriority), caseSensitive: false)
                .WithMessage("Priority must be one of: Low, Medium, High, Critical.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .When(x => x.DueDate.HasValue)
                .WithMessage("DueDate cannot be in the past.");
        }
    }
}
