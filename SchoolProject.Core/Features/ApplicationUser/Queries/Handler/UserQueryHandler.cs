using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Result;
using SchoolProject.Core.SharedResources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler
        , IRequestHandler<GetUserPaginateQuery, PaginatedResult<GetUserPaginateResponse>>
        , IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserQueryHandler(IStringLocalizer<SharedResource> stringLocalizer
            , UserManager<User> userManager, IMapper mapper): base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<GetUserPaginateResponse>> Handle(GetUserPaginateQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUserPaginateResponse>(users)
                                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) 
            { 
                return NotFound<GetUserByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
    }
}
