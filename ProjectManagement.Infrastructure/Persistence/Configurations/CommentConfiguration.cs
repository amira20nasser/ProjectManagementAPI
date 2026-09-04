using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Persistence.Configurations
{
    public class CommentConfiguration : BaseConfiguration<Comment,int>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);
        
        }

    }
}
