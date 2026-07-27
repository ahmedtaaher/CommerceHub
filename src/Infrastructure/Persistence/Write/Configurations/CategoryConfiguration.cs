using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations
{
  public class CategoryConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.ToTable("Categories");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedNever();

      builder.Property(x => x.Name).HasConversion(
        name => name.Value,
        value => CategoryName.Create(value).Value).HasMaxLength(CategoryName.MaxLength).IsRequired();

      builder.Property(x => x.Description).HasMaxLength(500);

      builder.HasIndex(x => x.Name).IsUnique();

      builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

      builder.Ignore(x => x.DomainEvents);
    }
  }
}