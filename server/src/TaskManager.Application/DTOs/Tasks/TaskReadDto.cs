using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs.Tasks;

public class TaskReadDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public Status Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? CompletedAt { get; init; }
    public required string CreatedById { get; init; }
}
