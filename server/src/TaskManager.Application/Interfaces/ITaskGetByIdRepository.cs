using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskGetByIdRepository
{
    Task<TaskEntity?> GetByIdAsync(int id);
}
