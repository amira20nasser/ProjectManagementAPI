namespace ProjectManagement.Application.Features.Tasks.UpdateTask
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = default!;
        public string Priority { get; set; } = default!;
        public Guid ProjectId { get; set; }
    }
}
