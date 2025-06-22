using TaskManager.Application.DTOs.Tasks;
using TaskManager.Application.Responses;

namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    Task<TaskReadDto?> GetByIdAsync(int id);
    Task<TaskReadDto> CreateAsync(TaskCreateDto dto);
    Task<bool> UpdateAsync(int id, TaskUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<PaginationResponse<TaskReadDto>> GetAllPagedAsync(TaskQueryParamsDto queryParams);
}
