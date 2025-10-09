using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Data.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class SignInCommand:IRequest<Response<JWTAuthResponse>>
    {
        [DefaultValue("")]
        public string UserName { get; set; }
        [DefaultValue("")]
        public string Password { get; set; }
    }
}
