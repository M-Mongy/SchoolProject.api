using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.student.Queries.Handler
{
    public class studentQueryHandler :ResponseHandler,
        IRequestHandler<GetStudentListQuery,Response<List<getStudentQueryListResponse>>>,
        IRequestHandler<GetStudentByIdQuery,Response<getSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
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

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> exception = e => new GetStudentPaginatedListResponse(e.StudID, e.Name,e.Address,e.Departments.DName);
            //var Querable=_service.GetStudentQueryable();
            var QuerableFilter = _service.FilterStudentPaginatedQuerable(request.orderBy,request.Search);
            var PaginatedList = await QuerableFilter.Select(exception).ToPaginatedListAsync(request.pageNumber, request.pageSize);
            return PaginatedList;
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
