using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class InventoryMovementConfiguration: IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.ToTable("InventoryMovements");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => InventoryMovementId.Create(v))
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasConversion(
                    v => v.Id,
                    v => ProductId.Create(v))
                .IsRequired();

            builder.Property(x => x.QuantityChanged).HasConversion<int>().IsRequired();

            builder.Property(x => x.Timestamp).HasColumnType("datetime2").IsRequired();

            builder.Property(x => x.Reason).HasMaxLength(250).IsRequired();

            builder.Property(x => x.MovementType).HasConversion<int>().IsRequired();

            // Relationships
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}