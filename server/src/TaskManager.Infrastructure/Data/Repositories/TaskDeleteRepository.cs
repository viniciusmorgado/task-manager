using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskDeleteRepository(TaskManagerContext context) : ITaskDeleteRepository
{
    private readonly TaskManagerContext _context = context;

    public async Task DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
