using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.GetTaskById
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
        public GetTaskByIdQuery(Guid id) => Id = id;
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly IGenericRepo<TaskItem, Guid> _taskRepo;

        public GetTaskByIdQueryHandler(IGenericRepo<TaskItem, Guid> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _taskRepo.QueryAsNoTracking()
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException($"Task with Id {request.Id} not found.");

            return new TaskDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Status = entity.Status.ToString(),
                Priority = entity.Priority.ToString(),
                ProjectId = entity.ProjectId,
                ProjectName = entity.Project?.Name,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
