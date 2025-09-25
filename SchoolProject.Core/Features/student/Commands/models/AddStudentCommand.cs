using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;

namespace SchoolProject.Core.Features.student.Commands.models
{
     public class AddStudentCommand : IRequest<Response<string>>
     {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string address { get; set; }
        public string phone  { get; set; }
        public int Department_id  { get; set; }

     }
}
