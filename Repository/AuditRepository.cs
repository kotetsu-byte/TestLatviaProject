using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Data;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Repository
{
    public class AuditRepository : IAuditRepository
    {
        private readonly TestDBContext _context;

        public AuditRepository(TestDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Audit>> GetAllAudits()
        {
            return await _context.AuditLogs.ToListAsync();
        }
    }
}
