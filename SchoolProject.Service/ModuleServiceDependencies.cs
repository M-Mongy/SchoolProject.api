using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Service.Absract;
using SchoolProject.Service.Implementations;
using SchoolProject.Service.Implemntation;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection addServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IstudentService, studentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationsService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddSingleton(new ConcurrentDictionary<string, JWTAuthResponse.RefreshToken>());
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            return services;
        }

    }
}
