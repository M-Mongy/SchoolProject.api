using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
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
    }
}
