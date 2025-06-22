#nullable disable
using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class Task : BaseEntity
{
    public Title Title { get; set; }
    public Description Description { get; set; }
    public Status Status { get; set; }
    public string CreatedById { get; init; }
    public User CreatedBy { get; init; }
    public DateTime? CompletedAt { get; set; }
    
    public Task() { }

    public Task(Title title, Description description, string createdById)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? new Description(string.Empty);
        Status = Status.Pending;
        CreatedById = createdById ?? throw new ArgumentNullException(nameof(createdById));
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(Title newTitle)
    {
        ArgumentNullException.ThrowIfNull(newTitle);
        if (Title.Equals(newTitle)) return;

        Title = newTitle;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(Description newDescription)
    {
        if (Description.Equals(newDescription)) return;

        Description = newDescription ?? new Description(string.Empty);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(Status newStatus)
    {
        if (Status == newStatus) return;

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;

        if (newStatus == Status.Concluded)
            CompletedAt = DateTime.UtcNow;
    }
}
