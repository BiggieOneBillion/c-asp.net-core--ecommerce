using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => OrderId.Create(v))
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasConversion(
                    v => v.Id,
                    v => UserId.Create(v))
                .IsRequired();

            builder.Property(x => x.PaymentId)
                .HasConversion(
                    v => v!.Id,
                    v => PaymentId.Create(v))
                .IsRequired();

            // Relationships
            builder.HasOne<Users>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Payment>()
                   .WithMany()
                   .HasForeignKey(x => x.PaymentId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}