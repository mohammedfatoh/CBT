using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class ApplicationUser  : IdentityUser
    {
        [Required]
        public GendreType Gendre { get; set;}

        [Required]
        public int Age { get; set;}

        
        [Required,MaxLength(100)]
        
        public string FullName { get; set;}

        public byte[] ProfilePicture { get; set; }

        public bool IsEnabled { get; set; }
    }
}
