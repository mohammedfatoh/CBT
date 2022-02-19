using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public float Amount { get; set; }

        public ICollection<Eximination> Eximinations { get; set; }
    }
}
