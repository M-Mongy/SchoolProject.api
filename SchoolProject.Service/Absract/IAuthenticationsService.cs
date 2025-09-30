using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Absract
{
    public interface IAuthenticationsService
    {
        public Task<string> GetJWTtoken(User user);
    }
}
