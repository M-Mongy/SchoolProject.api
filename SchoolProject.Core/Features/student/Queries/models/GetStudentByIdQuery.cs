using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.student.Queries.Results;

namespace SchoolProject.Core.Features.student.Queries.models
{
    public class GetStudentByIdQuery : IRequest<Response<getSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
