using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Data.DataConstraints;
using System.Xml.Linq;

namespace ToniAuto2003.Services.SalesMan.Model
{
    public class BecomeSalesManModel
    {
        [Required]
        [StringLength(phonenumberMaxLength, MinimumLength = phoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
