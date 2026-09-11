using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Features.Tasks.CreateTask;
using ProjectManagement.Application.Features.Tasks.DeleteTask;
using ProjectManagement.Application.Features.Tasks.GetTaskById;
using ProjectManagement.Application.Features.Tasks.GetTasks;
using ProjectManagement.Application.Features.Tasks.UpdateTask;

namespace ProjectManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ISender _sender;

        public TasksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? projectId, CancellationToken cancellationToken)
        {
            var query = new GetTasksQuery { ProjectId = projectId };
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTaskByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto dto, CancellationToken cancellationToken)
        {
            var command = new UpdateTaskCommand
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Status = dto.Status,
                Priority = dto.Priority,
                ProjectId = dto.ProjectId
            };
            var result = await _sender.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteTaskCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
