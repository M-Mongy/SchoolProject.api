using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Helper;
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
            services.AddSingleton(new ConcurrentDictionary<string, JWTAuthResponse.RefreshToken>());
            return services;
        }

    }
}
