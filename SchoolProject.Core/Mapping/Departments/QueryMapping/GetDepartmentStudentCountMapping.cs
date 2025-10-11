using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentStudentCountMapping()
        {
            CreateMap<ViewDepartment, GetDepartmentStudentCountResult>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));
        }
    }
}