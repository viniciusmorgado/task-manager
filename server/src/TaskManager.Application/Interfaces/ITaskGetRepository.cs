using TaskManager.Domain.Enumerators;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskGetRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync(string? title = null, Status? status = null);
}
