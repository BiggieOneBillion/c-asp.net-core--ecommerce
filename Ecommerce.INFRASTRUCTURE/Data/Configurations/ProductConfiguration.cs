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
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            
            builder.HasKey(p => p.ProductId.Value());
            
            builder.Property(p => p.ProductId.Value())
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();
            
            builder.Property(p => p.Description)
                .HasMaxLength(1000);
            
            builder.Property(p => p.CurrentPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            
            builder.Property(p => p.CategoryId.Value())
                .HasMaxLength(50)
                .IsRequired();
            
            // Relationships
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId.Value())
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}