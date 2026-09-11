using ProjectManagement.Application.Features.Common;

namespace ProjectManagement.Application.Features.Tasks.CreateTask
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; } = default!; // Low, Medium, High, Critical
        public Guid ProjectId { get; set; }
    }

    public class CreateTaskResponse : TaskDto { }
}
