namespace TaskManager.Application.DTOs.Tasks;

public class TaskCreateDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string CreatedById { get; set; }
}
