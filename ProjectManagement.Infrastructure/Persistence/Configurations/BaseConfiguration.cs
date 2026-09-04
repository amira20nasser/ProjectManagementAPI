using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Persistence.Configurations
{
    public class BaseConfiguration<TEntity,TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();
            builder.Property(entity => entity.UpdatedAt)
            .IsRequired(false);
        }
    }
}
