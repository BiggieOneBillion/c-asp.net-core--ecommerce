using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id)
                .HasConversion(
                    v => v.Id,
                    v => ProductId.Create(v))
                .IsRequired();
            
            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();
            
            builder.Property(p => p.Description)
                .HasMaxLength(1000);
            
            builder.Property(p => p.CurrentPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            builder.Property(p => p.CategoryId)
                .HasConversion(
                    v => v.Id,
                    v => CategoryId.Create(v))
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}