using MediatR;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Features.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<TaskDto>
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; } = default!;
        public Guid ProjectId { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly IGenericRepo<TaskItem, Guid> _taskRepo;
        private readonly IGenericRepo<Project, Guid> _projectRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(
            IGenericRepo<TaskItem, Guid> taskRepo,
            IGenericRepo<Project, Guid> projectRepo,
            IUnitOfWork unitOfWork)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepo.GetByIdAsync(request.ProjectId, cancellationToken);
            if (project is null)
                throw new KeyNotFoundException($"Project with Id {request.ProjectId} not found.");

            // Validation already handled by ValidationBehavior; safe to parse
            var priority = Enum.Parse<TaskPriority>(request.Priority, true);

            var entity = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = priority,
                Status = Domain.Enums.TaskItemStatus.Todo,
                ProjectId = request.ProjectId
            };

            await _taskRepo.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TaskDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Status = entity.Status.ToString(),
                Priority = entity.Priority.ToString(),
                ProjectId = entity.ProjectId,
                ProjectName = project.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
