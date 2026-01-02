using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.OrderItemsId);

            builder.Property(x => x.OrderItemsId)
                .HasConversion(
                    v => v.Id,
                    v => OrderItemsId.Create(v))
                .IsRequired();

            builder.Property(x => x.OrderId)
                .HasConversion(
                    v => v.Id,
                    v => OrderId.Create(v))
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasConversion(
                    v => v.Id,
                    v => ProductId.Create(v))
                .IsRequired();

            builder.Property(x => x.Quantity).HasConversion<int>().IsRequired();

            builder.Property(x => x.PricePerUnitAtPurchaseTime).HasColumnType("decimal(18,2)").IsRequired();

            // Relationships
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>()
                  .WithMany()
                  .HasForeignKey(x => x.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}