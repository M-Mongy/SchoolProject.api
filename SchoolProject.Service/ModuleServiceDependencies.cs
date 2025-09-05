using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Service.Absract;
using SchoolProject.Service.Implemntation;

namespace SchoolProject.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection addServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IstudentService, studentService>();
            return services;
        }

    }
}
