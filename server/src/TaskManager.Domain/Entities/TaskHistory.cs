#nullable disable
using TaskManager.Domain.Enumerators;

namespace TaskManager.Domain.Entities;

public class TaskHistory : BaseEntity
{
    public int TaskId { get; private set; }
    public Status PreviousStatus { get; private set; }
    public Status CurrentStatus { get; private set; }
    public string Description { get; private set; }
    public string UpdatedById { get; private set; }
    public User UpdatedBy { get; private set; }
    public Task Task { get; private set; }

    protected TaskHistory() { }

    public TaskHistory(int taskId, Status previousStatus, Status currentStatus, string description, string updatedById)
    {
        if (previousStatus == currentStatus)
            throw new ArgumentException("Previous status cannot be the same as current status.");

        TaskId = taskId;
        PreviousStatus = previousStatus;
        CurrentStatus = currentStatus;
        Description = description ?? string.Empty;
        UpdatedById = updatedById ?? throw new ArgumentNullException(nameof(updatedById));
        CreatedAt = DateTime.UtcNow;
    }
}
