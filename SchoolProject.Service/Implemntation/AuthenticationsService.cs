using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Absract;
using static SchoolProject.Data.Helper.JWTAuthResponse;

namespace SchoolProject.Service.Implemntation
{
    public class AuthenticationsService : IAuthenticationsService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string,RefreshToken> _ConcurrentDictionary;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AuthenticationsService(JwtSettings jwtSettings
                                      ,ConcurrentDictionary<string, RefreshToken> ConcurrentDictionary
                                      , IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _ConcurrentDictionary = ConcurrentDictionary;
            _refreshTokenRepository = refreshTokenRepository;

        }
        public async Task<JWTAuthResponse> GetJWTtoken(User user)
        {

            var claims=GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                 _jwtSettings.Issuer,
                 _jwtSettings.Audience,
                 claims,
                 expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken 
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = false,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
              await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response=new JWTAuthResponse ();
            response.AccessToken = accessToken;
            response.refreshToken = refreshToken;
            return response;
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GeneralRefreshToken(),
            };
            _ConcurrentDictionary.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GeneralRefreshToken()
        {
            var randomNumber= new byte[32];
            var NumberGenerate=RandomNumberGenerator.Create();
            NumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimsModel.UserName),user.UserName),
                new Claim(nameof(UserClaimsModel.Email),user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),

            };
            return claims;
        }

    }
}
