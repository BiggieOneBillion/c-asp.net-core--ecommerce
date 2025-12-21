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

            builder.HasKey(x => x.OrderItemsId.Value());

            builder.Property(x => x.OrderItemsId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.OrderId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.ProductId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Quantity).HasConversion<int>().IsRequired();

            builder.Property(x => x.PricePerUnitAtPurchaseTime).HasColumnType("decimal(18,2)").IsRequired();

            // Relationships
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId.Value())
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Product>()
                  .WithMany()
                  .HasForeignKey(x => x.ProductId.Value())
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}