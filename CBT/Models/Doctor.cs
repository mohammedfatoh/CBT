using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBT.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required ,MaxLength(100)]
        public string Name { get; set; }

        public GendreType Gendre { get; set; }

        public int Age { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Patient> patients { get; set; }



    }
}
