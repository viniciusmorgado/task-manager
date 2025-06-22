using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskGetByIdRepository(TaskManagerContext context) : ITaskGetByIdRepository
{
    public async Task<TaskEntity?> GetByIdAsync(int id)
    {
        return await context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
