using Microsoft.AspNetCore.Mvc;

namespace CBT.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
