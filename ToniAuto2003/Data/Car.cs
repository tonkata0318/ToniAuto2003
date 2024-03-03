using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;

namespace ToniAuto2003.Data
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int LeasingId { get; set; }

        [Required]
        [ForeignKey(nameof(LeasingId))]
        public Leasing Leasing { get ; set; } = null!;

    }
}