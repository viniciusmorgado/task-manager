namespace TaskManager.Application.DTOs;

public class TaskCreateDTO
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string CreatedById { get; set; }
}
