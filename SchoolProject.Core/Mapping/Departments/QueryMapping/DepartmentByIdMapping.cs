using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void DepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdQueryResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.DNameAr, src.DNameEn)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
            .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
            .ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.InstuctorList, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectInDepartment>()
                 .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.SubID))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            CreateMap<Student, StudentInDepartment>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor,InstructorInDepartment>()
                 .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.InsId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.ENameAr, src.ENameEn)));

        }
    }
}
