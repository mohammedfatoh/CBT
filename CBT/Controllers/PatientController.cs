using CBT.Data;
using CBT.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Examination(Eximination exmination,string IdUser)
        {
            if(!ModelState.IsValid)
                return View(exmination);
            try
            {
                string userId =IdUser;
                Patient patient=_context.Patients.FirstOrDefault(x => x.UserId == userId);
                if(patient!=null)
                {
                    exmination.patient_Id = patient.Id; 
                }
               await _context.Eximinations.AddAsync(exmination);
               await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(exmination);
            }

            return View();
        }

        public IActionResult PreviosExamination()
        {
            return View();
        }
      
    }
}
