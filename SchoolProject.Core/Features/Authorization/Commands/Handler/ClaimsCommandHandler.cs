using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public ClaimsCommandHandler(IStringLocalizer<SharedResource> stringLocalizer
            , IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaim(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
                case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
                case "FailedToUpdateClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateClaims]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}

