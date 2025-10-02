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
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Absract;

namespace SchoolProject.Core.Features.Authentication.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JWTAuthResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<JWTAuthResponse>>
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
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return The UserName Not Found
            if (user == null) return BadRequest<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);
            //try To Sign in 
            var signInResult = _signManager.CheckPasswordSignInAsync(user, request.Password, false);
            //if Failed Return Passord is wrong
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);

            //Generate Token
            var result = await _authenticationsService.GetJWTToken(user);
            //return Token 
            return Success(result);
        }

        public async Task<Response<JWTAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationsService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationsService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);
                case ("TokenIsNotExpired", null): return Unauthorized<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JWTAuthResponse>(_stringLocalizer[SharedResourcesKeys.PasswordOrUserNameNotCorrect]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JWTAuthResponse>();
            }
            var result = await _authenticationsService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }
    }
}
