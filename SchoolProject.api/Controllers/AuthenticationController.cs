using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.api.Base;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
