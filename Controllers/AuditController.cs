using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestLatviaProject.Interface;

namespace TestLatviaProject.Controllers
{
    [Authorize]
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
