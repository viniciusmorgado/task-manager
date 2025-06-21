using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enumerators;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskGetRepository : ITaskGetRepository
{
    private readonly TaskManagerContext _context;
    public TaskGetRepository(TaskManagerContext context) => _context = context;

    public async Task<IEnumerable<TaskEntity>> GetAllAsync(string? title = null, Status? status = null)
    {
        var query = _context.Tasks.AsQueryable();
        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(t => t.Title.Value.Contains(title));
        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);
        return await query.Include(t => t.TaskHistory).ToListAsync();
    }
}
