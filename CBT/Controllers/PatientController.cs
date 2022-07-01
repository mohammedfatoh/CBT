using CBT.Data;
using CBT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patagames.Ocr;
using Patagames.Ocr.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace CBT.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment hosting;
        public PatientController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IWebHostEnvironment hosting)
        {
            _context = context;
            _userManager = userManager;
            this.hosting = hosting;
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
                //check if patinnt hava cancer or no 

                if(exmination.RBCS < 1.5 && exmination.WBES < 1.5 && exmination.PLT <1.5)
                {
                    exmination.Result = "Cancer";
                }

                else
                {
                    exmination.Result = "healthy";
                }



               await _context.Eximinations.AddAsync(exmination);
               await _context.SaveChangesAsync();
                return RedirectToAction("ResultExamination", exmination);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(exmination);
            }

            return View();
        }

        public IActionResult ExaminationByImage()
        {
            return View("Exmination");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExaminationByImage(Eximination exmination, string IdUser)
        {
            if (!ModelState.IsValid)
                return View(exmination);
            try
            {
                string userId = IdUser;
                Patient patient = _context.Patients.FirstOrDefault(x => x.UserId == userId);
                if (patient != null)
                {
                    exmination.patient_Id = patient.Id;
                }

                //part upload image
                if (exmination.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "ExminationImages");
                }




                    List<string> word = new List<string>();
                using (var api = OcrApi.Create())
                {
                    api.Init(Languages.English);
                    string imgurl = ".\\wwwroot\\images\\1.png";
                    string plainText = api.GetTextFromImage(imgurl);
                    Console.WriteLine(plainText);
                    string rBC = "", redBloodCell = "", whitebloodCell = "", platelets = "";
                    for (int ind = 0; ind < plainText.Length; ind++)
                    {
                        if (char.IsLetter(plainText[ind]) || char.IsDigit(plainText[ind]))
                        {
                            rBC += char.ToLower(plainText[ind]);

                        }
                        else if (plainText[ind] == '.') { }
                        else if (rBC.Length > 0)
                        {
                            word.Add(rBC);
                            rBC = "";

                        }
                    }
                    for (int ind = 0; ind < word.Count; ind++)
                    {
                        if (ind + 2 < word.Count && (word[ind] == "red" || word[ind] == "r.b.cs" || word[ind] == "rbcs"))
                        {
                            ind += 3;
                            redBloodCell = word[ind];
                        }
                        if (word[ind] == "rdw")
                        {
                            whitebloodCell = word[ind + 1];
                        }
                        if (ind + 1 < word.Count && word[ind] == "platelet"
                             && word[ind + 1] == "count")
                        {
                            ind += 2;
                            platelets = word[ind];
                        }
                    }
                    exmination.RBCS= (float)Convert.ToDouble(redBloodCell); 
                    exmination.WBES= (float)Convert.ToDouble(whitebloodCell);
                    exmination.PLT = (float)Convert.ToDouble(platelets);
                    //check if patinnt hava cancer or no 

                    if (exmination.RBCS < 1.5 && exmination.WBES < 1.5 &&
                        exmination.PLT < 1.5)
                    {
                        exmination.Result = "Cancer";
                    }
                    else
                    {
                        exmination.Result = "healthy";
                    }

                    await _context.Eximinations.AddAsync(exmination);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Examination", exmination);
            }

            return View("Examination");
        }

        public IActionResult ResultExamination(Eximination eximination)
        {
            List<Treatment> treatments = _context.Treatments.ToList();
            ViewBag.Treatments = treatments;
            return View(eximination);
        }


        public async Task<IActionResult> PreviosExamination()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;
            
            Patient patient = _context.Patients.FirstOrDefault(x => x.UserId == userId);

            List<Eximination> eximinations = _context.Eximinations.Where(eximination => 
            eximination.patient_Id == patient.Id).ToList();
                

            return View(eximinations);
        }
      
    }
}
