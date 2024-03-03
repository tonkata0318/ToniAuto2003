using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ToniAuto2003.Data.DataConstraints;

namespace ToniAuto2003.Data
{
    public class SalesMan
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(phonenumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

    }
}
