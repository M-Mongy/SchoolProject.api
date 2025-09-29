using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
    public class AddUserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResource> _sharedResources;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public AddUserCommandHandler(IStringLocalizer<SharedResource> stringLocalizer
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
    }
}
