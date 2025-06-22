using AutoMapper;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskReadDto>> GetAllAsync(string? title, Status? status);
    Task<TaskReadDto?> GetByIdAsync(int id);
    Task<TaskReadDto> CreateAsync(TaskCreateDto dto);
    Task<bool> UpdateAsync(int id, TaskUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}

public class TaskService(
    ITaskPostRepository postRepo,
    ITaskPatchRepository patchRepo,
    ITaskGetRepository getRepo,
    ITaskGetByIdRepository getByIdRepo,
    ITaskDeleteRepository deleteRepo,
    IMapper mapper) : ITaskService
{

    public async Task<IEnumerable<TaskReadDto>> GetAllAsync(string? title, Status? status)
    {
        var tasks = await getRepo.GetAllAsync(title, status);
        return mapper.Map<IEnumerable<TaskReadDto>>(tasks);
    }

    public async Task<TaskReadDto?> GetByIdAsync(int id)
    {
        var task = await getByIdRepo.GetByIdAsync(id);
        return task == null ? null : mapper.Map<TaskReadDto>(task);
    }

    public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto)
    {
        var entity = mapper.Map<Task>(dto);
        await postRepo.AddAsync(entity);
        return mapper.Map<TaskReadDto>(entity);
    }

    public async Task<bool> UpdateAsync(int id, TaskUpdateDto dto)
    {
        var task = await getByIdRepo.GetByIdAsync(id);
        if (task == null) return false;

        task.UpdateTitle(new Title(dto.Title));
        task.UpdateDescription(new Description(dto.Description ?? string.Empty));
        task.ChangeStatus(dto.Status);

        await patchRepo.UpdateAsync(task, dto.UpdatedById);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await getByIdRepo.GetByIdAsync(id);
        if (task == null) return false;
        await deleteRepo.DeleteAsync(id);
        return true;
    }
}
