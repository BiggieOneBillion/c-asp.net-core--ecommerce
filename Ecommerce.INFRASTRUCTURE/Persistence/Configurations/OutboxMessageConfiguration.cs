using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.INFRASTRUCTURE.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(e => e.Content)
            .IsRequired();
            
        builder.HasIndex(e => e.ProcessedOn);
    }
}
