using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.InfraStructureBsaes;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}
