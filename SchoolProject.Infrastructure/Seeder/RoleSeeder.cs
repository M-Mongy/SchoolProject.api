using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _RoleManager)
        {
            var RolesCount = await _RoleManager.Roles.CountAsync();
            if(RolesCount <= 0)
            {
                await _RoleManager.CreateAsync(new Role()
                {
                    Name = "Admin",
                });          
                await _RoleManager.CreateAsync(new Role()
                {
                    Name = "User",
                });
            }
        }
    }
}
