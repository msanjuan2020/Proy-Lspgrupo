using Lpsgrupo.WebApi.Presentation.Interfaces;
using Lpsgrupo.WebApi.Presentation.Mappers;
using Lpsgrupo.WebApi.Presentation.MediatR.Queries;
using Lspgrupo.Cross.Entities.Common;
using Lspgrupo.Cross.Entities.Task;
using MediatR;

namespace Lpsgrupo.WebApi.Presentation.MediatR.Handlers
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, GenericResponse<List<TaskItemDto>>>
    {
        private readonly ITaskService _taskService;

        public GetAllTasksQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<GenericResponse<List<TaskItemDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            // Obtener las tareas
            var tasks = await _taskService.GetAll();

            if (tasks == null || tasks.Count == 0)
            {
                return GenericResponse<List<TaskItemDto>>.Fail("No se encontraron tareas.");
            }

            var taskItemDto = tasks.Select(task => task.MapGetTaskItemOutput()).ToList();

            return GenericResponse<List<TaskItemDto>>.Success(taskItemDto, "Tareas obtenidas correctamente.");
        }

    }
}
