using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Data.Repositories;

public class TaskGetRepository(TaskManagerContext context) : ITaskGetRepository
{
    public async Task<int> CountAsync(TaskQueryParamsDto queryParams)
    {
        var sql = "SELECT * FROM \"Tasks\" WHERE 1=1";
        var parameters = new List<object>();

        if (!string.IsNullOrWhiteSpace(queryParams.Title))
        {
            sql += " AND \"Title\" ILIKE {0}";
            parameters.Add($"%{queryParams.Title}%");
        }

        if (queryParams.Status.HasValue)
        {
            sql += $" AND \"Status\" = {{{parameters.Count}}}";
            parameters.Add((int)queryParams.Status.Value);
        }

        var query = context.Tasks.FromSqlRaw(sql, parameters.ToArray());
        return await query.CountAsync();
    }

    public async Task<IReadOnlyList<TaskEntity>> GetAllPagedAsync(TaskQueryParamsDto queryParams)
    {
        var sql = "SELECT * FROM \"Tasks\" WHERE 1=1";
        var parameters = new List<object>();

        if (!string.IsNullOrWhiteSpace(queryParams.Title))
        {
            sql += " AND \"Title\" ILIKE {0}";
            parameters.Add($"%{queryParams.Title}%");
        }

        if (queryParams.Status.HasValue)
        {
            sql += $" AND \"Status\" = {{{parameters.Count}}}";
            parameters.Add((int)queryParams.Status.Value);
        }

        sql += $" ORDER BY \"Id\" OFFSET {{{parameters.Count}}} LIMIT {{{parameters.Count + 1}}}";
        parameters.Add((queryParams.PageIndex - 1) * queryParams.PageSize);
        parameters.Add(queryParams.PageSize);

        var query = context.Tasks.FromSqlRaw(sql, parameters.ToArray());
        return await query.ToListAsync();
    }
}
