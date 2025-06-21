using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs.Tasks;

public class TaskReadDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public required string CreatedById { get; set; }
    public List<TaskHistoryReadDTO>? TaskHistory { get; set; }
}
