namespace TaskManager.Domain.Entities;

public class TaskHistory : BaseEntity
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public String PreviousStatus { get; set; }
    public String CurrentStatus { get; set; }
    public DateTime UpdatedAt { get; set; }
    // public User UpdatedBy { get; set; }
}
