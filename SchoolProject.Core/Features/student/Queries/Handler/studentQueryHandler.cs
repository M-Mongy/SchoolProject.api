using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.student.Queries.models;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Core.SharedResources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;
using SchoolProject.Service.Implemntation;

namespace SchoolProject.Core.Features.student.Queries.Handler
{
    public class studentQueryHandler :ResponseHandler,
        IRequestHandler<GetStudentListQuery,Response<List<getStudentQueryListResponse>>>,
        IRequestHandler<GetStudentByIdQuery,Response<getSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IstudentService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public studentQueryHandler(IstudentService service,
                                   IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer ) :base(stringLocalizer)
        {
            _service = service;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<getStudentQueryListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var response= await _service.GetStudentListAsync();
           var responseListMapper=_mapper.Map<List<getStudentQueryListResponse>>(response);
            return Success(responseListMapper);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            //Expression<Func<Student, GetStudentPaginatedListResponse>> exception = e => new GetStudentPaginatedListResponse(e.StudID, e.NameEn,e.Address,e.Department.DNameEn);
            var FilterQuery = _service.FilterStudentPaginatedQuerable(request.orderBy, request.Search);
            var PaginatedList = await _mapper.ProjectTo<GetStudentPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.pageNumber, request.pageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;

        }

        public async Task<Response<getSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _service.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<getSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<getSingleStudentResponse>(student);
            return Success(result);

        }
    }
}
