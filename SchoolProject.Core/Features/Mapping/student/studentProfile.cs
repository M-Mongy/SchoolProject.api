using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolProject.Core.Features.student.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Mapping.student
{
    public partial class studentProfile : Profile
    {
        public studentProfile()
        {
            getStudentListMapping();
        }
    }
}
