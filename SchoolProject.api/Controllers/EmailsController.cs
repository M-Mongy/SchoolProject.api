using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.api.Base;
using SchoolProject.Core.Features.Emails.Command.Models;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : AppControllerBase
    {
     
        [HttpPost(Router.EmailsRouting.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)

        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
