namespace TaskManager.Domain.Entities;

public class Task : BaseEntity
{

    public String Title { get; set; }
    public int Description { get; set; }

    public ICollection<TaskHistory> History { get; set; }
}
