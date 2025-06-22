using TaskManager.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPatchRepository(TaskManagerContext context) : ITaskPatchRepository
{
    // public async Task UpdateAsync(TaskEntity task, string updatedById)
    // {
    //     // var originalTask = await context.Tasks
    //     //     .AsNoTracking()
    //     //     .FirstOrDefaultAsync(t => t.Id == task.Id);
    //     //
    //     // if (originalTask == null)
    //     //     throw new InvalidOperationException($"Task with ID {task.Id} not found.");
    //
    //     context.Tasks.Update(task);
    //     await context.SaveChangesAsync();
    // }
    
    public async Task UpdateAsync(TaskEntity task, string updatedById)
    {
        Console.WriteLine($"Task ID: {task.Id}");
        Console.WriteLine($"Task Title: {task.Title?.Value}");
        Console.WriteLine($"Task Description: {task.Description?.Value}");
        Console.WriteLine($"Task Status: {task.Status}");

        context.Tasks.Update(task);
        var changes = await context.SaveChangesAsync();
        Console.WriteLine($"Changes saved: {changes}");
    }
}
