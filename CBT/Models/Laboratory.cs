using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class Laboratory
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public string Description { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string ImgUrl { get; set; }
    }
}
