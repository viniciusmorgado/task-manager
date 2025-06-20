using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Data;

public class TaskManagerContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    // public DbSet<Product> Products { get; init; }
    // public DbSet<Address> Addresses { get; set; }
    // public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    // public DbSet<Order> Orders { get; set; }
    // public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);
    }
}
