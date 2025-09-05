using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Queries.Handler
{
    public class studentHandler :ResponseHandler,IRequestHandler<GetStudentListQuery,Response<List<getStudentQueryListResponse>>>
    {
        private readonly IstudentService _service;
        private readonly IMapper _mapper;

        public studentHandler(IstudentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<Response<List<getStudentQueryListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var response= await _service.GetStudentListAsync();
           var responseListMapper=_mapper.Map<List<getStudentQueryListResponse>>(response);
            return Success(responseListMapper);
        }
    }
}
