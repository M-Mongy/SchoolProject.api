using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using System;

namespace SchoolProject.Infrastructure.Registration
{
    // Extension methods must be defined in a static class
    public static class IdentityServiceRegistration
    {
        // Renamed method to be more specific and follow PascalCase convention
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                // This setting requires a user to confirm their email address before they can sign in.
                options.SignIn.RequireConfirmedEmail = true;

                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            // Corrected DbContext name to follow convention
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}

