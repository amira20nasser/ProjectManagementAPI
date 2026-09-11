using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Projects.GetProjects
{
    public class GetProjectsQuery : IRequest<IReadOnlyList<ProjectDto>>
    {
    }

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IReadOnlyList<ProjectDto>>
    {
        private readonly IGenericRepo<Project, Guid> _projectRepo;

        public GetProjectsQueryHandler(IGenericRepo<Project, Guid> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<IReadOnlyList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _projectRepo.QueryAsNoTracking()
                .Include(p => p.Tasks)
                .ToListAsync(cancellationToken);

            return entities.Select(e => new ProjectDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                DueDate = e.DueDate,
                Status = e.Status.ToString(),
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                TasksCount = e.Tasks.Count
            }).ToList();
        }
    }
}
