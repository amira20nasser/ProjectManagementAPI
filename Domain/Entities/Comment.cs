
namespace ProjectManagement.Domain.Entities
{
    public class Comment : BaseEntity<int>
    {
        public string Content { get; set; } = default!;

        public Guid TaskId { get; set; }
        public TaskItem Task { get; set; } = default!;
    }
}
