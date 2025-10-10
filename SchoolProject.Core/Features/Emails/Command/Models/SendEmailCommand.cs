using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;

namespace SchoolProject.Core.Features.Emails.Command.Models
{
    public class SendEmailCommand:IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
