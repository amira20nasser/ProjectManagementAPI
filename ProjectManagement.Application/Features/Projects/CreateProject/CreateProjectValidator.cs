using FluentValidation;

namespace ProjectManagement.Application.Features.Projects.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .IsEnumName(typeof(ProjectManagement.Domain.Enums.ProjectStatus), caseSensitive: false)
                .WithMessage("Status must be one of: NotStarted, InProgress, Completed, OnHold, Cancelled.");

            RuleFor(x => x.DueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .When(x => x.DueDate.HasValue)
                .WithMessage("DueDate cannot be in the past.");
        }
    }
}
