using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.OrderId.Value());

            builder.Property(x => x.OrderId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.UserId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.PaymentId.Value()).HasMaxLength(50).IsRequired();

            // Relationships
            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(x => x.UserId.Value())
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Payment>()
                   .WithMany()
                   .HasForeignKey(x => x.PaymentId.Value())
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}