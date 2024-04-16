using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Infrastructure.Data
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
        [Column(TypeName = "decimal(18,2)")]
        //[Range(typeof(decimal), carsMinimalPrice , carsMinimalPrice , ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public string? RenterId { get; set; } = string.Empty;

        [Required]
        public int LeasingId { get; set; }

        [Required]
        [ForeignKey(nameof(LeasingId))]
        public Leasing Leasing { get ; set; } = null!;

    }
}