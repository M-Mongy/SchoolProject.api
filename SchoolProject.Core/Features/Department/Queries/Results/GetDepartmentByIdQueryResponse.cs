using System.Collections.Generic;

namespace SchoolProject.Core.Features.Department.Queries.Results
{
    public class GetDepartmentByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public List<StudentInDepartment> StudentList { get; set; }
        public List<SubjectInDepartment> SubjectList { get; set; }
        public List<InstructorInDepartment> InstuctorList { get; set; }
    }

    // Corrected class declaration (no parentheses) and Name property type
    public class StudentInDepartment
    {
        public int id { get; set; }
        public string Name { get; set; }
    }

    // Corrected class declaration (no parentheses) and Name property type
    public class SubjectInDepartment
    {
        public int id { get; set; }
        public string Name { get; set; }
    }

    // Corrected class declaration (no parentheses) and Name property type
    public class InstructorInDepartment
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}