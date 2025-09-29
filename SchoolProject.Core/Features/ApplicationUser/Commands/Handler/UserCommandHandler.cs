using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler
        ,IRequestHandler<AddUserCommand, Response<string>>
        ,IRequestHandler<UpdateUserCommand, Response<string>>
        ,IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UserCommandHandler(IStringLocalizer<SharedResource> stringLocalizer
                                     ,IMapper mapper
                                     ,UserManager<User> userManager) : base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager= userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) { return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]); }

            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userByUserName != null) { return BadRequest<string>(_sharedResources[SharedResourcesKeys.NameIsExist]); }

            var IdentityUser = _mapper.Map<User>(request);

            var createUser= await _userManager.CreateAsync(IdentityUser,request.Password);
            if (!createUser.Succeeded) {
                return BadRequest<string>(createUser.Errors.FirstOrDefault().Description);
            }

            return Created("");
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>();
            var newUser = _mapper.Map(request, oldUser);
            var result = await _userManager.UpdateAsync(newUser);
            if (!result.Succeeded) { return BadRequest<string>(_sharedResources[SharedResourcesKeys.NameIsExist]);}
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _userManager.FindByIdAsync(request.Id.ToString());
            if (User == null) return NotFound<string>();

            var result= await _userManager.DeleteAsync(User);
            if (!result.Succeeded) {return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeleteFailed]); }


            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }
    }
}
