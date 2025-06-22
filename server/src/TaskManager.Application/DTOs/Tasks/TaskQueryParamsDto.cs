using TaskManager.Domain.Enumerators;

namespace TaskManager.Application.DTOs.Tasks;

public class TaskQueryParamsDto
{
    public string? Title { get; set; }
    public Status? Status { get; set; }
    public int PageIndex { get; set; } = 1; // Default to first page
    public int PageSize { get; set; } = 10; // Default page size
}
