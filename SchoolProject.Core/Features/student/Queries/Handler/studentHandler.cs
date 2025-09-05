using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Queries.Handler
{
    public class studentHandler : IRequestHandler<GetStudentListQuery, List<Student>>
    {
        private readonly IstudentService _service;

        public studentHandler(IstudentService service)
        {
            _service = service;
        }

        public async Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentListAsync();
        }
    }
}
