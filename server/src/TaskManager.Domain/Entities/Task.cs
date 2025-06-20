#nullable disable
using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class Task : BaseEntity
{
    public Title Title { get; set; }
    public Description Description { get; set; }
    public Status Status { get; set; }
    public string CreatedById { get; set; }
    public User CreatedBy { get; set; }
    public DateTime? CompletedAt { get; set; }

    public ICollection<TaskHistory> TaskHistory { get; set; } = new List<TaskHistory>();

    public Task() { }

    public Task(Title title, Description description, string createdById)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? new Description(string.Empty);
        Status = Status.Pending;
        CreatedById = createdById ?? throw new ArgumentNullException(nameof(createdById));
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(Title newTitle, string updatedById)
    {
        if (newTitle == null) throw new ArgumentNullException(nameof(newTitle));

        Title = newTitle;
        UpdatedAt = DateTime.UtcNow;

        AddHistory(Status, Status, $"Title updated", updatedById);
    }

    public void UpdateDescription(Description newDescription, string updatedById)
    {
        Description = newDescription ?? new Description(string.Empty);
        UpdatedAt = DateTime.UtcNow;

        AddHistory(Status, Status, "Description updated", updatedById);
    }

    public void ChangeStatus(Status newStatus, string updatedById)
    {
        if (Status == newStatus) return;

        var oldStatus = Status;
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;

        if (newStatus == Status.Concluded)
            CompletedAt = DateTime.UtcNow;

        AddHistory(oldStatus, newStatus, $"Status changed from {oldStatus} to {newStatus}", updatedById);
    }

    private void AddHistory(Status previousStatus, Status currentStatus, string description, string updatedById)
    {
        var history = new TaskHistory(Id, previousStatus, currentStatus, description, updatedById);
        TaskHistory.Add(history);
    }
}
