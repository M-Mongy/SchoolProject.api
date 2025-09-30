using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfraStructureBsaes;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly DbSet<UserRefreshToken> _UserRefreshToken;

        public RefreshTokenRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _UserRefreshToken = dBContext.Set<UserRefreshToken>();
        }
        public ApplicationDBContext DBContext { get; }
    }
}
