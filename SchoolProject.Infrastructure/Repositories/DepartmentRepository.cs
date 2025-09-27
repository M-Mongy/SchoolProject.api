using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfraStructureBsaes;

namespace SchoolProject.Infrastructure.Repositories
{
    public  class DepartmentRepository: GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _students;

        public DepartmentRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _students = dBContext.Set<Department>();
        }
        public ApplicationDBContext DBContext { get; }
    }
}
