using Microsoft.AspNetCore.Mvc;
using SchoolProject.api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Data.AppMetaData;


namespace SchoolProject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
