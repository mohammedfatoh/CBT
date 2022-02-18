using Microsoft.AspNetCore.Mvc;

namespace CBT.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
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
