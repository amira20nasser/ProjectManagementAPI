namespace ProjectManagement.Application.Features.Projects.CreateProject
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = "NotStarted";
    }
}
