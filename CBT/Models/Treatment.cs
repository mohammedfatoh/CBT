using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBT.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public float Amount { get; set; }

        [ForeignKey("patient_Id")]
        public int patient_Id { get; set; }

        public virtual Patient Patient { get; set; }

        [ForeignKey("Exmination_id")]
        public int Exmination_id { get; set; }

        public virtual Eximination eximination { get; set; }
    }
}
