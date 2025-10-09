using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }

        public ManageUserRolesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
