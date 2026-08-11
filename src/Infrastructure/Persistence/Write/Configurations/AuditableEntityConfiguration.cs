using Domain.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations
{
  public static class AuditableEntityConfiguration
  {
    public static void Configure<TEntity, TId>(EntityTypeBuilder<TEntity> builder) where TEntity : AuditableEntity<TId>
    {
      builder.Property(x => x.CreatedBy).IsRequired(false);

      builder.Property(x => x.CreatedAt).IsRequired();

      builder.Property(x => x.LastModifiedBy).IsRequired(false);

      builder.Property(x => x.LastModifiedAt).IsRequired(false);

      builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);

      builder.Property(x => x.DeletedBy).IsRequired(false);

      builder.Property(x => x.DeletedAt).IsRequired(false);
    }
  }
}