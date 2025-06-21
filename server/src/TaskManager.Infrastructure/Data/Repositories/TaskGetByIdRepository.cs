using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskGetByIdRepository : ITaskGetByIdRepository
{
    private readonly TaskManagerContext _context;
    public TaskGetByIdRepository(TaskManagerContext context) => _context = context;

    public async Task<TaskEntity?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .Include(t => t.TaskHistory)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
