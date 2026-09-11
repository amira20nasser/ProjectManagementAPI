using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Features.Projects.CreateProject;
using ProjectManagement.Application.Features.Projects.GetProjectById;
using ProjectManagement.Application.Features.Projects.GetProjects;

namespace ProjectManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProjectsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetProjectsQuery();
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProjectByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}
