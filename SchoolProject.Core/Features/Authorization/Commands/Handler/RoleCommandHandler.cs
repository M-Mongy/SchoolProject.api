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
        IRequestHandler<EditRoleCommand, Response<string>>
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
    }
}
