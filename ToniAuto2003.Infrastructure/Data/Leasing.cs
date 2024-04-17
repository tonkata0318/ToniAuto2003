using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToniAuto2003.Infrastructure.Data
{
    public class Leasing
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;


        [Required]
        public decimal AmounthPerMonth { get; set; }

        [Required]
        public int Months { get; set; }

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;
    }
}
