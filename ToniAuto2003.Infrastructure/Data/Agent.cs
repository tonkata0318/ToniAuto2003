using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;
using Microsoft.EntityFrameworkCore;

namespace ToniAuto2003.Infrastructure.Data
{
    [Index(nameof(PhoneNumber),IsUnique = true)]
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(phonenumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public  List<Car> Cars { get; set; } = new List<Car>();
    }
}
