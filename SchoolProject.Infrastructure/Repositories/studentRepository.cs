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
    public class studentRepository : GenericRepositoryAsync<Student>, IstudentRepository
    {
        private readonly DbSet<Student> _students;
        
        public studentRepository(ApplicationDBContext dBContext):base(dBContext) 
        {
            _students = dBContext.Set<Student>();
        }

        public ApplicationDBContext DBContext { get; }

        public async Task<List<Student>> GetStudentListAsync()
        {
          return  await _students.Include(x=>x.Departments).ToListAsync();
        }
    }
}
