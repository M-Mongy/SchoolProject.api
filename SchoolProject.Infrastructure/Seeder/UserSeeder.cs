using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var users = await userManager.Users.CountAsync();
            if (users <= 0)
            {
                var defaultuser = new User()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "schoolProject",
                    Country = "Egypt",
                    PhoneNumber = "123456",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await userManager.CreateAsync(defaultuser, "Admin@123");
                await userManager.AddToRoleAsync(defaultuser, "Admin");
            }
            
        }
    }
}
