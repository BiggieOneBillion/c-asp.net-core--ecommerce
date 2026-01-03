using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ecommerce.CORE.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class InventoryConfiguration: IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => InventoryId.Create(v))
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasConversion(
                    v => v.Id,
                    v => ProductId.Create(v))
                .IsRequired();

            builder.Property(x => x.StockQuantity).HasConversion<int>().IsRequired();

            builder.Property(x => x.ReservedQuantity).HasConversion<int>().IsRequired();

            builder.HasIndex(x => x.ProductId).IsUnique();

            // builder.Property(x => x.InventoryType).HasConversion<int>().IsRequired();

            // Relationships
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}