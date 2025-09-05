using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.student.Queries.models
{
    public class GetStudentListQuery:IRequest<List<Student>>
    {

    }
}
