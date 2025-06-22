using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enumerators;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskGetRepository(TaskManagerContext context) : ITaskGetRepository
{
    public async Task<IEnumerable<TaskEntity>> GetAllAsync(string? title = null, Status? status = null)
    {
        var query = context.Tasks.AsQueryable();
        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(t => t.Title.Value.Contains(title));
        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);
        return await query.ToListAsync();
    }
}