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
    public class DeleteStudentCommand : IRequest<Response<string>>
    {

        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            Id = id;
        }

   

    }
}
