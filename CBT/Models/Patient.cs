using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class Patient
    {
        public int Id { get; set; }   
        
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(10)]
        public string Gendre { get; set; }

        public int Age { get; set; }

        public int doctor_id { get; set; }

    }
}
