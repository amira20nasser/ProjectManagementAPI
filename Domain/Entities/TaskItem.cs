using ProjectManagement.Domain.Enums;


namespace ProjectManagement.Domain.Entities
{
    public class TaskItem : BaseEntity<Guid>
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskItemStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
