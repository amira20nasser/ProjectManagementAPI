namespace ProjectManagement.Application.Features.Common
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TasksCount { get; set; }
    }
}
