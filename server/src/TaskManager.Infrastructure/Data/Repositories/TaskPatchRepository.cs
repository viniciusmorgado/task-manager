using TaskManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPatchRepository(TaskManagerContext context) : ITaskPatchRepository
{
    public async System.Threading.Tasks.Task UpdateAsync(TaskEntity task, string updatedById)
    {
        var originalTask = await context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == task.Id);

        if (originalTask == null)
            throw new InvalidOperationException($"Task with ID {task.Id} not found.");

        context.Tasks.Update(task);
        await context.SaveChangesAsync();
    }
}
