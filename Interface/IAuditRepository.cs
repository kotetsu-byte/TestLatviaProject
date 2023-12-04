using TestLatviaProject.Models;

namespace TestLatviaProject.Interface
{
    public interface IAuditRepository
    {
        Task<IEnumerable<Audit>> GetAllAudits();
    }
}
