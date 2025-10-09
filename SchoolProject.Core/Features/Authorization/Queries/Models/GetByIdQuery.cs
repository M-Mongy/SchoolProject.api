using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.Authorization.Queries.Result;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public  class GetByIdQuery:IRequest<Response<GetRoleByIdResult>>
    {
        public int Id { get; set; }

        public GetByIdQuery(int id)
        {
            Id = id;
        }
    }
}
