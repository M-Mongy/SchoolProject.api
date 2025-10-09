using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimQuery: IRequest<Response<ManageUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
