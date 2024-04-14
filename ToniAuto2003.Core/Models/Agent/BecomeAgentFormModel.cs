using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Core.Constants.MessageConstants;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(phonenumberMaxLength,MinimumLength =phoneNumberMinLength , ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string Phonenumber { get; set; } = null!;
    }
}
