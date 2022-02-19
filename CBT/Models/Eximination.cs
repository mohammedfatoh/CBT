using System.ComponentModel.DataAnnotations;

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

       
        public float   Result     { get; set; }

        public DateTime Createddate { get; set; }

        public int patient_Id { get; set; }

        public Patient Patient { get; set; }

        public ICollection<Treatment>  Treatments { get; set; }

    }
}
