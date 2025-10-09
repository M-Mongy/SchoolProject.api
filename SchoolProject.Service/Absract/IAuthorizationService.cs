using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Absract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<bool> IsRoleExistById(int RoleId);
        public Task<string> DeleteRoleAsync(int RoleId);
        public Task<string> EditRoleAsync(EditRoleRequest editRole);
        public Task<List<Role>> GetRoleslistAsync();
        public Task<Role> GetRoleByIdAsync(int id);

    }
}
