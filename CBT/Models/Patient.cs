using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class Patient
    {
        public int Id { get; set; }   
        
        [Required,MaxLength(100) ]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(10)]
        public GendreType Gendre  { get; set; }

        public int Age { get; set; }

        public int doctor_id { get; set; }

        public Doctor Doctor { get; set; }
        public List<Eximination> Eximinations { get; set; }

    }
}
