using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPostRepository(TaskManagerContext context) : ITaskPostRepository
{
    public async Task<int> AddAsync(TaskEntity task)
    {
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        return task.Id;
    }
}
