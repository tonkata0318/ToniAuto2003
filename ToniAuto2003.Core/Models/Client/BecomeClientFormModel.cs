using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static ToniAuto2003.Core.Constants.MessageConstants;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Core.Models.Client
{
    public class BecomeClientFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [Range(clientmoneyMin,clientmoneyMax,ErrorMessage = MoneyRequired)]
        [Display(Name = "Money")]
        public decimal Money { get; set; }
    }
}
