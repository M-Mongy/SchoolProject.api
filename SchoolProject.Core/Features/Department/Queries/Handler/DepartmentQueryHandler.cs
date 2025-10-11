using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Service.Absract;


namespace SchoolProject.Core.Features.Department.Queries.Handler
{
    public class DepartmentQueryHandler :ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>,
        IRequestHandler<GetDepartmentStudentCountQuery, Response<List<GetDepartmentStudentCountResult>>>,
         IRequestHandler<GetDepartmentStudentCountByIDQuery, Response<GetDepartmentStudentCountByIDResult>>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IMapper _mapper;

        public DepartmentQueryHandler(IDepartmentService departmentService,
            IMapper mapper,
            IStringLocalizer<SharedResource> stringLocalizer) : base(stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }

        public async Task<Response<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentService.GetDepartmentById(request.Id);
            if (response == null) return NotFound<GetDepartmentByIdQueryResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var mapper = _mapper.Map<GetDepartmentByIdQueryResponse>(response);
            return Success(mapper);
        }

        public async Task<Response<List<GetDepartmentStudentCountResult>>> Handle(GetDepartmentStudentCountQuery request, CancellationToken cancellationToken)
        {
            var viewDepartmentResult = await _departmentService.GetViewDepartmentDataAsync();
            var result = _mapper.Map<List<GetDepartmentStudentCountResult>>(viewDepartmentResult);
            return Success(result);
        }

        public async Task<Response<GetDepartmentStudentCountByIDResult>> Handle(GetDepartmentStudentCountByIDQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<DepartmentStudentCountProcParameters>(request);
            var procResult = await _departmentService.GetDepartmentStudentCountProcs(parameters);
            var result = _mapper.Map<GetDepartmentStudentCountByIDResult>(procResult.FirstOrDefault());
            return Success(result);
        }
    }
}
