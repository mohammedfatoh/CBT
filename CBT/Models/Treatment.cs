using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBT.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public float Amount { get; set; }

        public int orderOfCancer { get; set; }
    }
}
