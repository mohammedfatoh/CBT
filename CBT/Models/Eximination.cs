namespace CBT.Models
{
    public class Eximination
    {
        public int Id { get; set; }

        public string ImgUrl { get; set; }

        public float  RBCS      { get; set; }

        public float  PLT  { get; set; }

        public float    WBES    { get; set; }

        public float   Result     { get; set; }

        public DateTime Createddate { get; set; }

        public int patient_Id { get; set; }
}
}
