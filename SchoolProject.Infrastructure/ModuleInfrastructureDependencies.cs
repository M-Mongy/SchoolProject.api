using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.InfraStructureBsaes;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrustructure.Abstracts.Procedures;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.Repositories.Procedures;
using SchoolProject.Infrustructure.Repositories.Views;

namespace SchoolProject.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection addInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IstudentRepository, studentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            return services;
        }
    }
}
