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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.ToTable("Payments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => PaymentId.Create(v))
                .IsRequired();

            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");

            builder.Property(x => x.PaymentDate);

            builder.Property(x => x.OrderId)
                .HasConversion(
                    v => v.Id,
                    v => OrderId.Create(v))
                .IsRequired();

            builder.Property(x => x.PaymentType).HasConversion<int>().IsRequired();

            // Relationships
            builder.HasOne<Order>()
                .WithMany()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}