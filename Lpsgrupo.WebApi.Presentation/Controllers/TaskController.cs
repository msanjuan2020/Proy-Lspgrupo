using Lpsgrupo.WebApi.Presentation.Interfaces;
using Lpsgrupo.WebApi.Presentation.MediatR.Queries;
using Lpsgrupo.WebApi.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lpsgrupo.WebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllTasks()
        {
            var response = await _mediator.Send(new GetAllTasksQuery());
            return Ok(response);
        }
        //[HttpGet]
        //[Route("get-byid/{id}")]
        //public IActionResult GetTaskById(string id)
        //{
        //    var response = _taskService.GetById(id);
        //    return Ok(response);
        //}
        //[HttpPost]
        //[Route("create")]
        //public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        //{
        //    task.Id = task.Id = Guid.NewGuid().ToString();
        //     await _taskService.Create(task);
        //    return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);

        //}
        //[HttpPut]
        //[Route("update/{id}")]
        //public IActionResult UpdateTask(string id, [FromBody] TaskItem task)
        //{
        //    var response =_taskService.Update(id,task);
        //    return Ok(response);
        //}
        //[HttpDelete]
        //[Route("delete/{id}")]
        //public IActionResult DeleteTask(string id)
        //{
        //    var response = _taskService.Delete(id);
        //    return Ok(Response);
        //}
    }
}
