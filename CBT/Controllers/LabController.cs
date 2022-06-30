using CBT.Data;
using CBT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CBT.Controllers
{
    public class LabController : Controller
    {
        ApplicationDbContext context;

        public LabController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public IActionResult Index()
        {
            List<Laboratory> Labs = context.Laboratories.ToList();
            return View(Labs);
        }
    }
}
