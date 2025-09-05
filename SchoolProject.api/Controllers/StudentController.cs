using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.student.Queries.models;

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

        [HttpGet("/student/List")]
        public async Task<IActionResult> GetStudentList()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
        [HttpGet("/student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int  id)
        {
            var response = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(response);
        }
    }
}
