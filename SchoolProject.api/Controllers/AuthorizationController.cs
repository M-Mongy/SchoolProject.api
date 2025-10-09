using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddRolesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }        
        [HttpPost(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }       
        [HttpPost(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id )
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }       
        
       [HttpPost(Router.AuthorizationRouting.RoleList)]
        public async Task<IActionResult> RoleList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }
        [HttpPost(Router.AuthorizationRouting.RoleById)]
        public async Task<IActionResult> RoleById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetByIdQuery(id));
            return NewResult(response);
        }
    }
}
