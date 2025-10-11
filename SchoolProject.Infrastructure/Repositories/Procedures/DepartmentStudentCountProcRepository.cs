using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrustructure.Abstracts.Procedures;
using StoredProcedureEFCore;

namespace SchoolProject.Infrustructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        #region Fields
        private readonly ApplicationDBContext _context;
        #endregion

        #region Constructors
        public DepartmentStudentCountProcRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Handle Functions
        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProc>();
            await _context.LoadStoredProc("DepartmentStudentCountProc")  // Changed this line
                   .AddParam("DID", parameters.DID)  // Also hardcode parameter name
                   .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());
            return rows;
        }
        #endregion
    }
}