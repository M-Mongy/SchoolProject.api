using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRolesCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>, 
        IRequestHandler<UpdateUserRoleCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        public RoleCommandHandler(IStringLocalizer<SharedResource> stringLocalizer
            , IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;

        }

        public async Task<Response<string>> Handle(AddRolesCommand request, CancellationToken cancellationToken)
        {
            var result= await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Successfully") return Success(result);
              return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.addFailed]);


        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound")
                return NotFound<string>();
            else if (result == "Success")
                return Success((string)_stringLocalizer[SharedResourcesKeys.Success]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
                return NotFound<string>();
            else if (result == "Used") { return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.Used]); }
            else if (result == "Success")
                return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRole(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
