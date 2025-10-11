using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrustructure.Abstracts.Views;
using SchoolProject.Infrustructure.Repositories.Views;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IViewRepository<ViewDepartment> _viewDepartmentRepository;
        public DepartmentService(IDepartmentRepository DepartmentRepository,
            IViewRepository<ViewDepartment> viewDepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
            _viewDepartmentRepository = viewDepartmentRepository;
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

        public async Task<List<ViewDepartment>> GetViewDepartmentDataAsync()
        {
            var viewDepartment = await _viewDepartmentRepository.GetTableNoTracking().ToListAsync();
            return viewDepartment;
        }

        public async Task<bool> IsDepartmentIdExist(int DepartmentId)
        {
            return await _DepartmentRepository.GetTableNoTracking().AnyAsync(x => x.DID.Equals(DepartmentId));
        }
    }
}
