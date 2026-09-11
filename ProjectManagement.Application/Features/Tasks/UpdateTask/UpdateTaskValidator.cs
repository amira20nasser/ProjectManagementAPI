using FluentValidation;

namespace ProjectManagement.Application.Features.Tasks.UpdateTask
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Status)
                .NotEmpty()
                .IsEnumName(typeof(ProjectManagement.Domain.Enums.TaskItemStatus), caseSensitive: false)
                .WithMessage("Status must be one of: Todo, InProgress, Completed, Cancelled.");
            RuleFor(x => x.Priority)
                .NotEmpty()
                .IsEnumName(typeof(ProjectManagement.Domain.Enums.TaskPriority), caseSensitive: false)
                .WithMessage("Priority must be one of: Low, Medium, High, Critical.");
        }
    }
}
