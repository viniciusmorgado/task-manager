using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPatchRepository(TaskManagerContext context) : ITaskPatchRepository
{
    public async Task UpdateAsync(TaskEntity task, string updatedById)
    {
        context.Tasks.Update(task);
        var changes = await context.SaveChangesAsync();
        Console.WriteLine($"Changes saved: {changes}");
    }
}
