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
        public Task<JWTAuthResponse> GetJWTtoken(User user);
    }
}
