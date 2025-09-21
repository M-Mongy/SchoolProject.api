using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Mapping.student
{
    public partial class studentProfile
    {
        public void getStudentListMapping() 
        {
            CreateMap<Student, getStudentQueryListResponse>()
        .ForMember(x => x.departmentName, opt => opt.MapFrom(src => src.Departments.DName));
        }
    }
}
