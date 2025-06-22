using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPatchRepository(TaskManagerContext context) : ITaskPatchRepository
{
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
