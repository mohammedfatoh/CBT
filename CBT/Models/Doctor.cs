﻿using System.ComponentModel.DataAnnotations;

namespace CBT.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public GendreType Gendre { get; set; }

        public int Age { get; set; }


    }
}
