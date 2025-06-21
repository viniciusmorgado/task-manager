using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs;

public class TaskUpdateDTO
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public string? UpdatedById { get; set; }
}
