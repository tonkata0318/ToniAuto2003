using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Core.Constants.MessageConstants;

namespace ToniAuto2003.Core.Models.Leasing
{
    public class LeasingFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        public decimal AmounthPerMonth { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int Months { get; set; }
    }
}
