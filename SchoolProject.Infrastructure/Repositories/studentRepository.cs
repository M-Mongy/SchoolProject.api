using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure.Repositories
{
    public class studentRepository : IstudentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        
        public studentRepository(ApplicationDBContext dBContext)
        {
          _dbContext = dBContext;
        }

        public ApplicationDBContext DBContext { get; }

        public async Task<List<Student>> GetStudentListAsync()
        {
          return  await _dbContext.students.Include(x=>x.Departments).ToListAsync();
        }
    }
}
