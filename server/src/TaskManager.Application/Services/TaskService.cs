using AutoMapper;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Responses;
using TaskManager.Domain.ValueObjects;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services;

public class TaskService(
    ITaskPostRepository postRepo,
    ITaskPatchRepository patchRepo,
    ITaskGetRepository getRepo,
    ITaskGetByIdRepository getByIdRepo,
    ITaskDeleteRepository deleteRepo,
    IMapper mapper) : ITaskService
{

    public async Task<PaginationResponse<TaskReadDto>> GetAllPagedAsync(TaskQueryParamsDto queryParams)
    {
        var totalCount = await getRepo.CountAsync(queryParams);
        var tasks = await getRepo.GetAllPagedAsync(queryParams);
        var data = mapper.Map<IReadOnlyList<TaskReadDto>>(tasks);

        return new PaginationResponse<TaskReadDto>(queryParams.PageIndex, queryParams.PageSize, totalCount, data);
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

        if (dto.UpdatedById != null) await patchRepo.UpdateAsync(task, dto.UpdatedById);
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
