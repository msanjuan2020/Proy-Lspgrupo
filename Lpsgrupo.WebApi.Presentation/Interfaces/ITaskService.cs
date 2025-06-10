using Lpsgrupo.WebApi.Presentation.Models;

namespace Lpsgrupo.WebApi.Presentation.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem> GetById(string id);
        Task Create(TaskItem task);
        Task Update(string id, TaskItem task);
        Task Delete(string id);
    }
}
