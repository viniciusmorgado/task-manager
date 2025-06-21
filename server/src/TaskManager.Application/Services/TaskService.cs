using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskReadDTO>> GetAllAsync(string? title, Status? status);
    Task<TaskReadDTO?> GetByIdAsync(int id);
    Task<TaskReadDTO> CreateAsync(TaskCreateDTO dto);
    Task<bool> UpdateAsync(int id, TaskUpdateDTO dto);
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
    private readonly ITaskPostRepository _postRepo = postRepo;
    private readonly ITaskPatchRepository _patchRepo = patchRepo;
    private readonly ITaskGetRepository _getRepo = getRepo;
    private readonly ITaskGetByIdRepository _getByIdRepo = getByIdRepo;
    private readonly ITaskDeleteRepository _deleteRepo = deleteRepo;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<TaskReadDTO>> GetAllAsync(string? title, Status? status)
    {
        var tasks = await _getRepo.GetAllAsync(title, status);
        return _mapper.Map<IEnumerable<TaskReadDTO>>(tasks);
    }

    public async Task<TaskReadDTO?> GetByIdAsync(int id)
    {
        var task = await _getByIdRepo.GetByIdAsync(id);
        return task == null ? null : _mapper.Map<TaskReadDTO>(task);
    }

    public async Task<TaskReadDTO> CreateAsync(TaskCreateDTO dto)
    {
        var entity = _mapper.Map<Task>(dto);
        await _postRepo.AddAsync(entity);
        return _mapper.Map<TaskReadDTO>(entity);
    }

    public async Task<bool> UpdateAsync(int id, TaskUpdateDTO dto)
    {
        var task = await _getByIdRepo.GetByIdAsync(id);
        if (task == null) return false;

        task.UpdateTitle(new Title(dto.Title));
        task.UpdateDescription(new Description(dto.Description ?? string.Empty));
        task.ChangeStatus(dto.Status);

        await _patchRepo.UpdateAsync(task, dto.UpdatedById);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _getByIdRepo.GetByIdAsync(id);
        if (task == null) return false;
        await _deleteRepo.DeleteAsync(id);
        return true;
    }
}