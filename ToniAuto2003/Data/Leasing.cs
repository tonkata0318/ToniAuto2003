﻿using System.ComponentModel.DataAnnotations;

namespace ToniAuto2003.Data
{
    public class Leasing
    {
        public int Id { get; set; }

        [Required]
        public decimal AmounthPerMonth { get; set; }

        [Required]
        public int Months { get; set; }
    }
}
