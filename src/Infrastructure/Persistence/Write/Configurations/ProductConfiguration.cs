using Domain.Catalog.Entities;
using Domain.Catalog.ValueObjects;
using Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Write.Configurations
{
  public class ProductConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder.ToTable("Products");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Id).ValueGeneratedNever();

      builder.Property(x => x.Name).HasConversion(name => name.Value, value => ProductName.Create(value).Value).HasMaxLength(200).IsRequired();

      builder.Property(x => x.Description).HasConversion(description => description.Value, value => ProductDescription.Create(value).Value).HasMaxLength(2000);

      builder.Property(x => x.Sku).HasConversion(sku => sku.Value, value => Sku.Create(value).Value).HasMaxLength(50).IsRequired();

      builder.HasIndex(x => x.Sku).IsUnique();

      builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(30).IsRequired();

      builder.OwnsOne(x => x.Price, money =>
      {
        money.Property(x => x.Amount).HasColumnName("Price").HasPrecision(18, 2).IsRequired();

        money.Property(x => x.Currency).HasConversion(currency => currency.Code, code => Currency.Create(code).Value).HasColumnName("Currency").HasMaxLength(3).IsRequired();
      });

      builder.Ignore(x => x.DomainEvents);
    }
  }
}