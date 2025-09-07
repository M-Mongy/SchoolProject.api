using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Absract
{
    public interface IstudentService
    {
        public Task<List<Student>> GetStudentListAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<bool> IsNameExsit(string Name);
        public Task<bool> IsNameExsitExclusive(string Name,int id);

    }
}
