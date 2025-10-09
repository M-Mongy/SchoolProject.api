using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class RefreshTokenCommand: IRequest<Response<JWTAuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
