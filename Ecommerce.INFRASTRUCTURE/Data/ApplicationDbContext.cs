using System;

namespace Ecommerce.INFRASTRUCTURE.Data;

using Microsoft.EntityFrameworkCore;
using Ecommerce.CORE.Entity;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Users> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Inventory> Inventories { get; set; } = null!;
    public DbSet<Order> Orders{ get; set; } = null!;
    public DbSet<OrderItems> OrderItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;

    // do not forget to enable audit trail
    // public DbSet<Invitation> Invitations { get; set; } = null!;
  
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatically update UpdatedAt timestamp
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);
        
        // foreach (var entry in entries)
        // {
        //     if (entry.Entity is Users user)
        //         user.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is Workspace workspace)
        //         workspace.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is ProjectEntity project)
        //         project.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is TaskEntity task)
        //         task.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is Comment comment)
        //         comment.UpdatedAt = DateTime.UtcNow;
        //     else if (entry.Entity is Note note)
        //         note.UpdatedAt = DateTime.UtcNow;
        // }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}



