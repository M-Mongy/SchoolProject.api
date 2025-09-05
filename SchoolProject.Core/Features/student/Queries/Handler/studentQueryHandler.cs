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
    public class studentQueryHandler :ResponseHandler,
        IRequestHandler<GetStudentListQuery,Response<List<getStudentQueryListResponse>>>,
        IRequestHandler<GetStudentByIdQuery,Response<getSingleStudentResponse>>
    {
        private readonly IstudentService _service;
        private readonly IMapper _mapper;

        public studentQueryHandler(IstudentService service, IMapper mapper)
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

        async Task<Response<getSingleStudentResponse>> IRequestHandler<GetStudentByIdQuery, Response<getSingleStudentResponse>>.Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _service.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<getSingleStudentResponse>("Object not found");
            var result = _mapper.Map<getSingleStudentResponse>(student);
            return Success(result);

        }
    }
}
