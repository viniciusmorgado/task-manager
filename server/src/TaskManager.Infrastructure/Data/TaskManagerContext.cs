#nullable disable
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data;

public class TaskManagerContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);
    }
}
