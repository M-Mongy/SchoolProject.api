using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> AddRoleAsync(string roleName)
        {
            var IdentityRole = new Role();
            IdentityRole.Name = roleName;
            var result =await _roleManager.CreateAsync(IdentityRole);
            if (result.Succeeded)
                return "Successfully";
            return "Failed";


        }

        public async Task<string> EditRoleAsync(EditRoleRequest editRole)
        {
            var role = await _roleManager.FindByIdAsync(editRole.Id.ToString());
            if (role == null)
                return "NotFound";
            role.Name=editRole.Name;
           var result= await _roleManager.UpdateAsync(role);
            if (result.Succeeded) { return "Successfully"; }
            var error = string.Join("-",result.Errors);
             return error;
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            //var role= _roleManager.FindByNameAsync(roleName);
            //if (role == null) return false;
            //return true;
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
