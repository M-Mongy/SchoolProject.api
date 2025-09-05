using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Repositories;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
      public static IServiceCollection addInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IstudentRepository,studentRepository>();
            return services; 
        }
    }
}
