using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBT.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(10)]
        public GendreType Gendre  { get; set; }

        [Required]
        public int Age { get; set; }

        [ForeignKey("doctor_id")]
        public int doctor_id { get; set; }

        public virtual  Doctor Doctor { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Eximination> eximinations { get; set; }

        public virtual ICollection<Treatment> treatments { get; set; }


    }
}
