using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.DTOs;

namespace SchoolProject.Service.Absract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(int RoleId);
        public Task<string> DeleteRoleAsync(int RoleId);
        public Task<string> EditRoleAsync(EditRoleRequest editRole);

    }
}
