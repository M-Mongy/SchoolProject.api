using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Absract
{
    public interface IAuthenticationsService
    {
        public Task<JWTAuthResponse> GetJWTToken(User user);
        public Task<JWTAuthResponse> GetRefreshToken(string accessToken, string refreshToken);
        public Task<string> ValidateToken(string AccessToken);
    }
}
