using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Tasks.GetTasks
{
    public class GetTasksQuery : IRequest<IReadOnlyList<TaskDto>>
    {
        public Guid? ProjectId { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IReadOnlyList<TaskDto>>
    {
        private readonly IGenericRepo<TaskItem, Guid> _taskRepo;

        public GetTasksQueryHandler(IGenericRepo<TaskItem, Guid> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var query = _taskRepo.QueryAsNoTracking().Include(t => t.Project).AsQueryable();

            if (request.ProjectId.HasValue)
                query = query.Where(t => t.ProjectId == request.ProjectId.Value);

            var entities = await query.ToListAsync(cancellationToken);

            return entities.Select(e => new TaskDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                DueDate = e.DueDate,
                Status = e.Status.ToString(),
                Priority = e.Priority.ToString(),
                ProjectId = e.ProjectId,
                ProjectName = e.Project?.Name,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToList();
        }
    }
}
