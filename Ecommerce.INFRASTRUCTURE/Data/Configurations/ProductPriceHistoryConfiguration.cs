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
    public class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
    {
        public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)   
        {

            builder.ToTable("ProductPriceHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => ProductPriceHistoryId.Create(v))
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasConversion(
                    v => v.Id,
                    v => ProductId.Create(v))
                .IsRequired();

            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.EffectiveDate).HasColumnType("datetime2");

            // Relationships
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}