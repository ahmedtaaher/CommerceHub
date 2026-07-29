using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configurations
{
  public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
  {
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
      builder.ToTable("RefreshTokens");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.TokenHash).IsRequired().HasMaxLength(256);

      builder.Property(x => x.CreatedAt).IsRequired();

      builder.Property(x => x.ExpiresAt).IsRequired();

      builder.Property(x => x.RevokedReason).HasMaxLength(500);

      builder.Property(x => x.ReplacedByTokenHash).HasMaxLength(256);

      builder.HasIndex(x => x.TokenHash).IsUnique();

      builder.HasIndex(x => x.UserId);
    }
  }
}