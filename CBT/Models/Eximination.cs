using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBT.Models
{
    public class Eximination
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public float  RBCS      { get; set; }

        [Required]
        public float  PLT  { get; set; }

        [Required]
        public float    WBES    { get; set; }

        [Required]
        public float hemoglobin { get; set; }

        [Required]
        public float MCHC { get; set; }

        [Required]
        public float MCH { get; set; }

        public string   Result     { get; set; }

        public string NamePatient { get; set; }

        public string EmailPatient { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        [Required]
        public DateTime Createddate { get; set; }

        [ForeignKey("patient_Id")]
        public int patient_Id { get; set; }

        public  virtual Patient Patient { get; set; }

    }
}
