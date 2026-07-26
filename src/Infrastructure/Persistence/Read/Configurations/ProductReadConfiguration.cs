using Infrastructure.Persistence.Read.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Read.Configurations
{
  public class ProductReadConfiguration : IEntityTypeConfiguration<ProductReadModel>
  {
    public void Configure(EntityTypeBuilder<ProductReadModel> builder)
    {
      builder.ToTable("Products");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedNever();

      builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

      builder.Property(x => x.Description).HasMaxLength(2000);

      builder.Property(x => x.Sku).HasMaxLength(50).IsRequired();

      builder.HasIndex(x => x.Sku).IsUnique();

      builder.Property(x => x.Price).HasPrecision(18, 2);

      builder.Property(x => x.Currency).HasMaxLength(3).IsRequired();

      builder.Property(x => x.Status).HasMaxLength(30).IsRequired();
    }
  }
}