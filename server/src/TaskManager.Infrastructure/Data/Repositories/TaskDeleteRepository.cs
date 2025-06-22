using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskDeleteRepository(TaskManagerContext context) : ITaskDeleteRepository
{
    public async Task DeleteAsync(int id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task != null)
        {
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }
    }
}
