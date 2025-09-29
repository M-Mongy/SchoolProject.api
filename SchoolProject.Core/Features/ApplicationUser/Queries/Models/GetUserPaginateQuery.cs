using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Queries.Result;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginateQuery:IRequest<PaginatedResult<GetUserPaginateResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
