using System;

namespace Ecommerce.INFRASTRUCTURE.Data;

using Microsoft.EntityFrameworkCore;
using Ecommerce.CORE.Entity;
using Ecommerce.INFRASTRUCTURE.Persistence;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Inventory> Inventories { get; set; } = null!;
    public DbSet<Order> Orders{ get; set; } = null!;
    public DbSet<OrderItems> OrderItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<ProductPriceHistory> ProductPriceHistories {get; set;} = null!;
    public DbSet<InventoryMovement> InventoryMovements { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;

    // do not forget to enable audit trail
    // public DbSet<Invitation> Invitations { get; set; } = null!;
  
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // 1. Collect domain events from aggregate roots
        var domainEvents = ChangeTracker
            .Entries<CORE.Common.IAggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Any())
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        // 2. Convert to outbox messages
        var outboxMessages = domainEvents.Select(domainEvent => new OutboxMessage
        {
            Id = Guid.NewGuid(),
            // Type = domainEvent.GetType().AssemblyQualifiedName!,
            Type = domainEvent.EventType().ToString().ToLowerInvariant(),
            Content = System.Text.Json.JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
            OccurredOn = domainEvent.OccurredOn
        }).ToList();

        if (outboxMessages.Any())
        {
            await OutboxMessages.AddRangeAsync(outboxMessages, cancellationToken);
        }

        // 3. Clear domain events after capturing
        foreach (var entry in ChangeTracker.Entries<CORE.Common.IAggregateRoot>())
        {
            entry.Entity.ClearDomainEvents();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}



