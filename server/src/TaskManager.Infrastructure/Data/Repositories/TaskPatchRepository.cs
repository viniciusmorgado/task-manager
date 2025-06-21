using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskPatchRepository(TaskManagerContext context) : ITaskPatchRepository
{
    private async TaskHistory AddHistoryIfChanged(TaskEntity original, TaskEntity updated)
    {
        var histories = new List<TaskHistory>();

        // Histórico de mudança de título
        if (!original.Title.Equals(updated.Title))
        {
            histories.Add(new TaskHistory(
                updated.Id,
                original.Status,
                updated.Status,
                $"Title changed from '{original.Title}' to '{updated.Title}'",
                updated.CreatedById
            ));
        }

        // Histórico de mudança de descrição
        if (!original.Description.Equals(updated.Description))
        {
            histories.Add(new TaskHistory(
                updated.Id,
                original.Status,
                updated.Status,
                $"Description changed",
                updated.CreatedById
            ));
        }

        // Histórico de mudança de status
        if (original.Status != updated.Status)
        {
            histories.Add(new TaskHistory(
                updated.Id,
                original.Status,
                updated.Status,
                $"Status changed from {original.Status} to {updated.Status}",
                updated.CreatedById
            ));
        }

        if (histories.Any())
        {
            context.TaskHistories.AddRange(histories);
        }
    }

    public Task UpdateAsync(TaskEntity task, string updatedById)
    {
        throw new NotImplementedException();
    }
}
