using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class AuthenticationsService : IAuthenticationsService
    {
        private readonly JwtSettings _jwtSettings;
        public AuthenticationsService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public Task<string> GetJWTtoken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimsModel.UserName),user.UserName),
                new Claim(nameof(UserClaimsModel.Email),user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber),user.PhoneNumber),

            };
            var jwtToken = new JwtSecurityToken(
                 _jwtSettings.Issuer,
                 _jwtSettings.Audience,
                 claims,
                 expires: DateTime.Now.AddMinutes(2),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);
        }
    }
}
