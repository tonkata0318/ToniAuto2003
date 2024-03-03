using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ToniAuto2003.Data.DataConstraints;

namespace ToniAuto2003.Data
{
    public class Clients
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        public decimal Money { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
