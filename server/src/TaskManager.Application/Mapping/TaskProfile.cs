using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.ValueObjects;
using TaskEntity = TaskManager.Domain.Entities.Task;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskReadDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.TaskHistory, opt => opt.MapFrom(src => src.TaskHistory));

        CreateMap<TaskHistory, TaskHistoryReadDTO>();

        CreateMap<TaskCreateDTO, TaskEntity>()
            .ConstructUsing(dto => new TaskEntity(
                new Title(dto.Title),
                new Description(dto.Description ?? string.Empty),
                dto.CreatedById
            ));
    }
}
