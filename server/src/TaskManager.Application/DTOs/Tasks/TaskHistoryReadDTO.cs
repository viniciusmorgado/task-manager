using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs;

public class TaskHistoryReadDTO
{
    public int Id { get; set; }
    public Status PreviousStatus { get; set; }
    public Status CurrentStatus { get; set; }
    public required string Description { get; set; }
    public string? UpdatedById { get; set; }
    public DateTime CreatedAt { get; set; }
}
