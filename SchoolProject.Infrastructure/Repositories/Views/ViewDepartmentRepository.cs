﻿using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfraStructureBsaes;
using SchoolProject.Infrustructure.Abstracts.Views;

namespace SchoolProject.Infrustructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        #region Fields
        private DbSet<ViewDepartment> viewDepartment;
        #endregion

        #region Constructors
        public ViewDepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            viewDepartment = dbContext.Set<ViewDepartment>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}