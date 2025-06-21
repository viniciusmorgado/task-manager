using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Interfaces;

public interface ITaskPatchRepository
{
    Task UpdateAsync(TaskEntity task, string updatedById);
}
