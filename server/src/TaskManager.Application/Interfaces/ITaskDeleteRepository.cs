namespace TaskManager.Application.Interfaces;

public interface ITaskDeleteRepository
{
    Task DeleteAsync(int id);
}
