using Microsoft.AspNetCore.Mvc;
using TestLatviaProject.Interface;

namespace TestLatviaProject.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditRepository _auditRepository;

        public AuditController(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task<IActionResult> Index()
        {
            var audits = await _auditRepository.GetAllAudits();
            
            return View(audits);
        }
    }
}
