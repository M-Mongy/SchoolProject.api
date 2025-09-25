using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.student.Queries.Results
{
    public class getStudentQueryListResponse
    {
        public int StudID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Address { get; set; }
        public string? departmentName { get; set; }
    }
}
