using CBT.Models;
using System.ComponentModel.DataAnnotations;

namespace CBT.ViewModels
{
    public class AddUserViewModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
       

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gendre")]
        public GendreType Gendre { get; set; }


        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string UserName { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
