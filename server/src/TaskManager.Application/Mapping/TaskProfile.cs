using AutoMapper;
using TaskManager.Application.DTOs.Tasks;
using TaskManager.Domain.ValueObjects;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Application.Mapping;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskReadDto>()
            .ForMember(dest => 
                dest.Title, opt => 
                    opt.MapFrom(src => src.Title.Value))
            .ForMember(
                dest => dest.Description, opt => 
                    opt.MapFrom(src => src.Description!.Value));
        
        CreateMap<TaskCreateDto, TaskEntity>()
            .ConstructUsing(dto => new TaskEntity(
                new Title(dto.Title),
                new Description(dto.Description ?? string.Empty),
                dto.CreatedById
            ));
    }
}
