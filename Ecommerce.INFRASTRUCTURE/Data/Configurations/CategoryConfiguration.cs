using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Data.Configurations
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(
                    v => v.Id,
                    v => CategoryId.Create(v))
                .IsRequired();

            builder.Property(x => x.CategoryName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.CategoryDescription).HasMaxLength(500);

            builder.Property(x => x.ActiveStatus).HasConversion<int>().IsRequired();
        }
    }
    
}