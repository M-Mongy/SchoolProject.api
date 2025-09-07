using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Data.AppMetaData;
using SchoolProject.Data.Entities;

namespace SchoolProject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        [HttpGet(Router.StudentRouting.GetByID)]
        public async Task<IActionResult> GetStudentById([FromRoute] int  id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(response);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)

        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand(id));
            return Ok(response);
        }

    }
}
