using Infrastructure.Persistence.Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Read.Configurations
{
  public class CategoryReadConfiguration : IEntityTypeConfiguration<CategoryReadModel>
  {
    public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
    {
      builder.ToTable("Categories");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

      builder.Property(x => x.Description).HasMaxLength(500);

      builder.HasIndex(x => x.Name).IsUnique();
    }
  }
}