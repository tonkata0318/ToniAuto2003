using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Core.Constants.MessageConstants;

namespace ToniAuto2003.Core.Models.Leasing
{
    public class LeasingServiceModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public decimal AmountPerMonth { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int Months { get; set; }
    }
}
