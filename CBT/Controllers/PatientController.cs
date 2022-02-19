using CBT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CBT.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IActionResult Index()
        {

            return View();
        }
        public IActionResult Examination()
        {
            return View();
        }

        public IActionResult PreviosExamination()
        {
            return View();
        }
      
    }
}
