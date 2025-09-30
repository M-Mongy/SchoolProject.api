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
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JWTAuthResponse>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IAuthenticationsService _authenticationsService;
        public AuthenticationCommandHandler(
             IStringLocalizer<SharedResource> stringLocalizer
            ,UserManager<User> userManager
            ,SignInManager<User> SignManager
            , IAuthenticationsService authenticationsService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signManager = SignManager;
            _authenticationsService = authenticationsService;
        }

        public async Task<Response<JWTAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user =await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.NameIsNotExist]);

            var signInResult = _signManager.CheckPasswordSignInAsync(user, request.Password,false);

            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);


            var AccessToken =await _authenticationsService.GetJWTtoken(user);
            return Success(AccessToken);
        }

 
    }
}
