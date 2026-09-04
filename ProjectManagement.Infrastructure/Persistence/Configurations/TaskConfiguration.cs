using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ProjectManagement.Domain.Entities;


namespace ProjectManagement.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : BaseConfiguration<TaskItem, Guid>
    {
        public override void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            base.Configure(builder);
            builder.HasMany(e => e.Comments)
                .WithOne(e => e.Task)
                .HasForeignKey(e=>e.TaskId);
        }
    }
}
