using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Mappings;

public class TaskHistoryMap : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.ToTable("TaskHistory");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PreviousStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CurrentStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.TaskId)
            .IsRequired();

        builder.Property(x => x.UpdatedById)
            .IsRequired()
            .HasMaxLength(450);

        builder.HasOne(x => x.Task)
            .WithMany(x => x.TaskHistory)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UpdatedBy)
            .WithMany()
            .HasForeignKey(x => x.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
