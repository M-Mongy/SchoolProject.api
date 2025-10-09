using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Result;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Absract;
using SchoolProject.Service.Implemntation;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
        IRequestHandler<GetByIdQuery, Response<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>
 
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public RoleQueryHandler(
            IStringLocalizer<SharedResource> stringLocalizer,
            IAuthorizationService authorizationService,
            IMapper mapper,
            UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }


        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRoleslistAsync();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null) return NotFound<GetRoleByIdResult>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var result = await _authorizationService.GetManageUserRolesData(user);
            return Success(result);
        }
    }
}
