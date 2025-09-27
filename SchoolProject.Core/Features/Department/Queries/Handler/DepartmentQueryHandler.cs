using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Core.SharedResources;
using SchoolProject.Service.Absract;


namespace SchoolProject.Core.Features.Department.Queries.Handler
{
    public class DepartmentQueryHandler :ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdQueryResponse>>
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
    }
}
