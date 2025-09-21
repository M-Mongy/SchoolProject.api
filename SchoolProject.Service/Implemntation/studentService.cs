using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Absract;

namespace SchoolProject.Service.Implemntation
{
    public class studentService : IstudentService
    {
        private readonly IstudentRepository _repository;

        public studentService(IstudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AddAsync(Student student)
        {

            if (student.StudID == null)
            {
            }
            _repository.AddAsync(student);
            return "Added Successfully ";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            await _repository.DeleteAsync(student);
            return "Deleted Successfully ";
        }

        public async Task<string> EditAsync(Student student)
        {
            await _repository.UpdateAsync(student);
            return "Success";
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderEnum , string Search)
        {
           var FilterPaginated= _repository.GetTableNoTracking().Include(x=>x.Departments).AsQueryable();
            if (Search != null)
            {
                FilterPaginated = FilterPaginated.Where(x => x.Name.Contains(Search) || x.Address.Contains(Search));
            }

            switch (orderEnum)
            {
                case StudentOrderingEnum.StudID:
                    FilterPaginated = FilterPaginated.OrderBy(x => x.StudID);   
                    break;
                case StudentOrderingEnum.Address:
                    FilterPaginated = FilterPaginated.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.Name:
                    FilterPaginated = FilterPaginated.OrderBy(x => x.Name);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    FilterPaginated = FilterPaginated.OrderBy(x => x.Departments.DName);
                    break;
            }

            return FilterPaginated;

        }

        public Task<Student> GetStudentByIdAsync(int id)
        {
            var student = _repository.GetTableNoTracking().Include(x => x.Departments)
                .Where(x => x.StudID.Equals(id)).FirstOrDefaultAsync();

            return student;
        }

        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _repository.GetStudentListAsync();
        }

        public IQueryable<Student> GetStudentQueryable()
        {
           return _repository.GetTableNoTracking().Include(x=>x.Departments).AsQueryable(); 
        }

        public async Task<bool> IsNameExsit(string Name)
        {
            var studentResult = _repository.GetTableNoTracking().Where(x => x.Name.Equals(Name)).FirstOrDefault();
            if (studentResult == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsNameExsitExclusive(string name, int id)
        {
            var studentResult = await _repository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name && x.StudID != id);

            return studentResult != null;
        }

    }
}
