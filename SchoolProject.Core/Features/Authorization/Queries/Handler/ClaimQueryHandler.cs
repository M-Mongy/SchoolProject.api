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
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Authorization.Queries.Handler
{
    public class ClaimQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimQuery, Response<ManageUserClaimsResult>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public ClaimQueryHandler(IStringLocalizer<SharedResource> stringLocalizer,
                                  IAuthorizationService authorizationService,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            var result = await _authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
        #endregion
    }
}

