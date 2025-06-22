using TaskManager.Application.DTOs.Tasks;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskGetRepository
{
    Task<int> CountAsync(TaskQueryParamsDto queryParams);
    Task<IReadOnlyList<TaskEntity>> GetAllPagedAsync(TaskQueryParamsDto queryParams);
}
