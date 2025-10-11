using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile:Profile
    {
        public DepartmentProfile() 
        {
            DepartmentByIdMapping();
            GetDepartmentStudentCountMapping();
            GetDepartmentStudentCountByIdMapping();
        }
    }
}
