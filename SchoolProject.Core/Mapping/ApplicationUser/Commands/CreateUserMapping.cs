using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public void CreateUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}
