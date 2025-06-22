using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Data.Repositories;

namespace TaskManager.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTaskManagerServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<ITaskPostRepository, TaskPostRepository>();
        services.AddScoped<ITaskPatchRepository, TaskPatchRepository>();
        services.AddScoped<ITaskGetRepository, TaskGetRepository>();
        services.AddScoped<ITaskGetByIdRepository, TaskGetByIdRepository>();
        services.AddScoped<ITaskDeleteRepository, TaskDeleteRepository>();
    }
}
