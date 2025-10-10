using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Absract
{
    public interface IEmailsService
    {
        public Task<string> SendEmail(string Email,string Message);
    }
}
