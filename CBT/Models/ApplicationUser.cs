using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class ApplicationUser  : IdentityUser
    {
        public GendreType Gendre { get; set;}

        public int Age { get; set;}

        [MaxLength(100)]
        public string name { get; set;}

    }
}
