using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Token)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(rt => rt.FamilyId)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(rt => rt.UserId)
            .HasConversion(
                v => v.Id,
                v => UserId.Create(v))
            .IsRequired();

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);
        
        builder.HasIndex(rt => rt.Token)
            .IsUnique();
        
        builder.HasIndex(rt => rt.FamilyId);
    }
}
