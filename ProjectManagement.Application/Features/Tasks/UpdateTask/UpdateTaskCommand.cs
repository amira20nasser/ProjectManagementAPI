using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Features.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public Guid ProjectId { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
    {
        private readonly IGenericRepo<TaskItem, Guid> _taskRepo;
        private readonly IGenericRepo<Project, Guid> _projectRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(
            IGenericRepo<TaskItem, Guid> taskRepo,
            IGenericRepo<Project, Guid> projectRepo,
            IUnitOfWork unitOfWork)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _taskRepo.Query().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity is null)
                throw new KeyNotFoundException($"Task with Id {request.Id} not found.");

            if (entity.ProjectId != request.ProjectId)
            {
                var project = await _projectRepo.GetByIdAsync(request.ProjectId, cancellationToken);
                if (project is null)
                    throw new KeyNotFoundException($"Project with Id {request.ProjectId} not found.");
                entity.ProjectId = request.ProjectId;
            }

            // Validation via ValidationBehavior guarantees valid enum names
            var status = Enum.Parse<TaskItemStatus>(request.Status, true);
            var priority = Enum.Parse<TaskPriority>(request.Priority, true);

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.DueDate = request.DueDate;
            entity.Status = status;
            entity.Priority = priority;

            _taskRepo.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var projectName = (await _projectRepo.GetByIdAsync(entity.ProjectId, cancellationToken))?.Name;

            return new TaskDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Status = entity.Status.ToString(),
                Priority = entity.Priority.ToString(),
                ProjectId = entity.ProjectId,
                ProjectName = projectName,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
