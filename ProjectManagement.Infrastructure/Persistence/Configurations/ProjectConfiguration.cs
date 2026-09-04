using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : BaseConfiguration<Project,Guid>
    {
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            base.Configure(builder);

            builder.HasMany(e => e.Tasks)
                .WithOne(e=>e.Project)
                .HasForeignKey(e=>e.ProjectId);
        }

    }
}
