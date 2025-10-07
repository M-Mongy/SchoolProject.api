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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler
        ,IRequestHandler<AddUserCommand, Response<string>>
        ,IRequestHandler<UpdateUserCommand, Response<string>>
        ,IRequestHandler<DeleteUserCommand, Response<string>>
        ,IRequestHandler<ChangeUserPasswordCommand, Response<string>>
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
            var users= await _userManager.Users.ToListAsync();
            if (users.Count >= 0)
            {
                await _userManager.AddToRoleAsync(IdentityUser, "User");
            }
            else
            {
                await _userManager.AddToRoleAsync(IdentityUser, "Admin");
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

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
          

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        }
    }
}
