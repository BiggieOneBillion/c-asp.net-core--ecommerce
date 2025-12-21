using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class ProductPriceHistoryConfiguration : IEntityTypeConfiguration<ProductPriceHistory>
    {
        public void Configure(EntityTypeBuilder<ProductPriceHistory> builder)   
        {

            builder.ToTable("ProductPriceHistory");

            builder.HasKey(x => x.ProductPriceHistoryId.Value());

            builder.Property(x => x.ProductPriceHistoryId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.ProductId.Value()).HasMaxLength(50).IsRequired();

            builder.Property(x => x.NewPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.EffectiveDate).HasColumnType("datetime2");

            // Relationships
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId.Value())
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}