using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;

    }
}
