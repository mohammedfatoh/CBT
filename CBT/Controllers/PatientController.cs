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
using IronOcr;

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
                    //exmination.Patient = _context.Patients.FirstOrDefault(x => x.UserId == userId);
                    exmination.NamePatient = patient.Name;
                    var user = await _userManager.FindByIdAsync(IdUser);
                    exmination.EmailPatient = user.Email;
                }
                //check if patinnt hava cancer or no 

                if (exmination.RBCS < 4 && exmination.RBCS >3 && exmination.WBES < 20 && exmination.WBES > 16 &&
                            exmination.PLT < 150)
                {
                    exmination.Result = "firstcancer";
                }
                else if ((exmination.RBCS >= 1 && exmination.RBCS < 2) && (exmination.WBES >= 1 && exmination.WBES < 2) &&
                    (exmination.PLT >= 1 && exmination.PLT < 2))
                {
                    exmination.Result = "Secondcancer";
                }
                else if ((exmination.RBCS >= 2 && exmination.RBCS < 3) && (exmination.WBES >= 2 && exmination.WBES < 3) &&
                    (exmination.PLT >= 2 && exmination.PLT < 3))
                {
                    exmination.Result = "Thirdcancer";
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
                    //exmination.Patient=new Patient();
                    //exmination.Patient= _context.Patients.FirstOrDefault(x => x.UserId == userId);
                    exmination.NamePatient = patient.Name;
                    var user = await _userManager.FindByIdAsync(IdUser);
                    exmination.EmailPatient = user.Email;
                }

                //part upload image
                string fileName = string.Empty;
                if (exmination.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "ExminationImages");
                    fileName = exmination.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    exmination.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    exmination.ImgUrl = fullPath;
                    Console.WriteLine(fullPath);

                    //code analyze image

                    List<string> word = new List<string>();
                    var Ocr = new IronTesseract();
                    using (var Input = new OcrInput(fullPath))
                    {
                       // api.Init(Languages.English);
                        string imgurl = fullPath;
                        Input.Deskew();
                        // string plainText = api.GetTextFromImage(imgurl);
                        var plainText1 = Ocr.Read(Input);
                        string plainText = plainText1.Text;

                        Console.WriteLine(plainText);
                        string rBC = "", redBloodCell = "", whitebloodCell = "", platelets = "";
                        for (int ind = 0; ind < plainText.Length; ind++)
                        {
                            if (char.IsLetter(plainText[ind]) || char.IsDigit(plainText[ind]))
                            {
                                rBC += char.ToLower(plainText[ind]);

                            }
                            else if (plainText[ind] == '.') { rBC += plainText[ind]; }
                            else if (rBC.Length > 0)
                            {
                                word.Add(rBC);
                                rBC = "";

                            }
                        }
                        bool f = true, f1 = true, f2 = true;
                        for (int ind = 0; ind < word.Count; ind++)
                        {
                            if (f && ind + 2 < word.Count && (word[ind] == "red" || word[ind] == "r.b.cs" || word[ind] == "rbcs" || word[ind] == "rbc"))
                            {
                                if (word[ind] == "r.b.cs" || word[ind] == "rbcs" || word[ind] == "rbc") ind++;
                                else ind += 3;
                                redBloodCell = word[ind];
                                f = false;
                            }
                            if (f1 &&( word[ind] == "rdw" || word[ind] == "rdws" ||word[ind] == "row"))
                            {
                                f1 = false;
                                whitebloodCell = word[ind + 1];
                            }
                            if (f2 && ind + 1 < word.Count && (word[ind] == "platelets" || word[ind] == "platelet" 
                                || word[ind] == "plt"))
                            {
                                f2 = false;
                                ind += 1;
                                if (word[ind] == "count")
                                    ind++;
                                platelets = word[ind];
                            }
                        }

                        Console.WriteLine(redBloodCell);
                        Console.WriteLine(whitebloodCell);
                        Console.WriteLine(platelets);

                        exmination.RBCS = (float)Convert.ToDouble(redBloodCell);
                        exmination.WBES = (float)Convert.ToDouble(whitebloodCell);
                        exmination.PLT = (float)Convert.ToDouble(platelets);
                        //check if patinnt hava cancer or no 

                        if (exmination.RBCS < 1 && exmination.WBES < 1 &&
                            exmination.PLT < 1)
                        {
                            exmination.Result = "firstCancer";
                        }
                        else if((exmination.RBCS >= 1 && exmination.RBCS < 2 ) && (exmination.WBES >= 1 && exmination.WBES < 2) &&
                            (exmination.PLT >= 1 && exmination.PLT < 2))
                        {
                            exmination.Result = "SecondCancer";
                        }
                        else if ((exmination.RBCS >= 2 && exmination.RBCS < 3) && (exmination.WBES >= 2 && exmination.WBES < 3) &&
                            (exmination.PLT >= 2 && exmination.PLT < 3))
                        {
                            exmination.Result = "ThirdCancer";
                        }
                        else
                        {
                            exmination.Result = "healthy";
                        }

                        await _context.Eximinations.AddAsync(exmination);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ResultExamination", exmination);
                    }
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
            List<Treatment> treatments;
            if (eximination.Result.ToLower().Equals("firstCancer"))
            {
                 treatments = _context.Treatments.Where
                    (treatment => treatment.orderOfCancer ==1).ToList();
                ViewBag.Treatments = treatments;
            }
            else if (eximination.Result.ToLower().Equals("SecondCancer"))
            {
                treatments = _context.Treatments.Where
                    (treatment => treatment.orderOfCancer == 2).ToList();
                ViewBag.Treatments = treatments;
            }
            else if (eximination.Result.ToLower().Equals("ThirdCancer"))
            {
                treatments = _context.Treatments.Where
                    (treatment => treatment.orderOfCancer == 3).ToList();
                ViewBag.Treatments = treatments;
            }
            
            return View(eximination);
        }


        public async Task<IActionResult> PreviosExamination()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            string userId = user.Id;
            List<Eximination> eximinations;
            if (( await _userManager.IsInRoleAsync(user,RolesNames.RoleAdmin)) || 
                ( await _userManager.IsInRoleAsync(user, RolesNames.RoleDoctor)))
            {
                eximinations = _context.Eximinations.ToList();
                return View(eximinations);
            }
            else
            {
                Patient patient = _context.Patients.FirstOrDefault(x => x.UserId == userId);

                eximinations = _context.Eximinations.Where(eximination =>
                eximination.patient_Id == patient.Id).ToList();


                return View(eximinations);
            }
        }
      
    }
}
