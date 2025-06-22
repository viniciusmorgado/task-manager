using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs.Tasks;

public class TaskUpdateDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public string? UpdatedById { get; set; }
}
