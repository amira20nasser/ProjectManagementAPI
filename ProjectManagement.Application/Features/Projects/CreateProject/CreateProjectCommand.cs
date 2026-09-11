using MediatR;
using ProjectManagement.Application.Abstraction.Repo;
using ProjectManagement.Application.Features.Common;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;

namespace ProjectManagement.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = "NotStarted";
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IGenericRepo<Project, Guid> _projectRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IGenericRepo<Project, Guid> projectRepo, IUnitOfWork unitOfWork)
        {
            _projectRepo = projectRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // Validation via ValidationBehavior guarantees valid enum name
            var status = Enum.Parse<ProjectStatus>(request.Status, true);

            var entity = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                DueDate = request.DueDate,
                Status = status
            };

            await _projectRepo.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                DueDate = entity.DueDate,
                Status = entity.Status.ToString(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TasksCount = 0
            };
        }
    }
}
