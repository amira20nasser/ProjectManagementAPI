using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Projects.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public Guid Id { get; set; }
        public GetProjectByIdQuery(Guid id) => Id = id;
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IGenericRepo<Project, Guid> _projectRepo;

        public GetProjectByIdQueryHandler(IGenericRepo<Project, Guid> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _projectRepo.QueryAsNoTracking()
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException($"Project with Id {request.Id} not found.");

            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Status = entity.Status.ToString(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TasksCount = entity.Tasks.Count
            };
        }
    }
}
