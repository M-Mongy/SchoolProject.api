using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.student.Queries.models
{
    public class GetStudentPaginatedListQuery:IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int pageNumber { get; set; }
        public int pageSize  { get; set; }
        public StudentOrderingEnum orderBy { get; set; }
        public string? Search { get; set; }
    }
}
