using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskPostRepository
{
    Task<int> AddAsync(TaskEntity task);
}
