using Lpsgrupo.WebApi.Presentation.Models;
using Lspgrupo.Cross.Entities.Task;

namespace Lpsgrupo.WebApi.Presentation.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItemDto MapGetTaskItemOutput(this TaskItem taskItem)
        {
            if (taskItem == null)
            {
                return null;
            }
            return new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                IsCompleted = taskItem.IsCompleted,

            };
        }
    }
}
