using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Features.Mapping.student
{
    public partial class studentProfile
    {
        public void GetStudentListMapping() {
            CreateMap<AddStudentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.Department_id))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));

        }
    }

}
