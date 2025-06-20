#nullable disable
namespace TaskManager.Domain.Entities;

public class TaskHistory : BaseEntity
{
    public int TaskId { get; set; }
    public String PreviousStatus { get; set; }
    public String CurrentStatus { get; set; }
    // public User UpdatedBy { get; set; } // TODO: This should be the user Id.
}
