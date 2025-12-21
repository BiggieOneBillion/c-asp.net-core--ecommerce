using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class InventoryMovementConfiguration: IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.ToTable("InventoryMovements");

            builder.HasKey(x => x.InventoryMovementId.Value());

            builder.Property(x => x.InventoryMovementId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.ProductId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.QuantityChanged).HasConversion<int>().IsRequired();

            builder.Property(x => x.Timestamp).HasColumnType("datetime2").IsRequired();

            builder.Property(x => x.Reason).HasMaxLength(250).IsRequired();

            builder.Property(x => x.MovementType).HasConversion<int>().IsRequired();

            // Relationships
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId.Value())
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}