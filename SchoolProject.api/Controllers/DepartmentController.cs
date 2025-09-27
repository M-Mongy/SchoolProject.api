using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.api.Base;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmentById([FromRoute]  int id)
        {
            var response = await Mediator.Send(new GetDepartmentByIdQuery(id));
            return Ok(response);
        }

    }
}
