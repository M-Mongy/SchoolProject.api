using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository; 
        public DepartmentService(IDepartmentRepository DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }

        public Task<Department> GetDepartmentById(int id)
        {
            var department = _DepartmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                .Include(x => x.Instructor)
                .Include(x => x.Instructors)
                .Include(x => x.Students).FirstOrDefaultAsync();
            return department;
        }
    }
}
