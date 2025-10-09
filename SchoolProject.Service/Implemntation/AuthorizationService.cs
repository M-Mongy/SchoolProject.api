using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _UserManager;
        private readonly ApplicationDBContext _dbContext;
        public AuthorizationService(RoleManager<Role> roleManager
            , UserManager<User> userManager, ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _UserManager = userManager;
            _dbContext = dbContext;
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

        public async Task<bool> IsRoleExistByName(string roleName)
        {
            //var role= _roleManager.FindByNameAsync(roleName);
            //if (role == null) return false;
            //return true;
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<string> DeleteRoleAsync(int RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());
            if (role == null) return "NotFound";
            var users = await _UserManager.GetUsersInRoleAsync(role.Name);
            if (users != null && users.Count()>0) { return "Used"; }

           var result= await _roleManager.DeleteAsync(role);
            if (result.Succeeded) { return "Successfully"; }
            var error = string.Join("-", result.Errors);
            return error;

        }
        public async Task<bool> IsRoleExistById(int RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());
            if (role == null) return false;
            else return true;
        }

        public async Task<List<Role>> GetRoleslistAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<ManageUserRolesResult> GetManageUserRolesData(User user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //userroles
            var userRoles = await _UserManager.GetRolesAsync(user);
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolesList.Add(userrole);
            }
            response.userRoles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserRole(UpdateUserRoleRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _UserManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //get user Old Roles
                var userRoles = await _UserManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _UserManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesresult = await _UserManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded)
                    return "FailedToAddNewRoles";
                await transact.CommitAsync();
                //return Result
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
    }
}
