using Lpsgrupo.WebApi.Presentation.Models;
using Lspgrupo.Cross.Entities.Common;
using Lspgrupo.Cross.Entities.Task;
using MediatR;

namespace Lpsgrupo.WebApi.Presentation.MediatR.Queries
{
    public record GetAllTasksQuery() : IRequest<GenericResponse<List<TaskItemDto>>>
    {

    }
}
