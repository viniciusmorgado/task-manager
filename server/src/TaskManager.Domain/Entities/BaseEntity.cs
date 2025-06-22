namespace TaskManager.Domain.Entities;

public class BaseEntity
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; protected set; }
}
