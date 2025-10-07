using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class AddRolesCommand :IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
