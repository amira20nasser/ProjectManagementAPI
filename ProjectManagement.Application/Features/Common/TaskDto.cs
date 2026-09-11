namespace ProjectManagement.Application.Features.Common
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
