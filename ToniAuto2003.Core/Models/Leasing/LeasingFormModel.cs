using System.ComponentModel.DataAnnotations;

namespace ToniAuto2003.Core.Models.Leasing
{
    public class LeasingFormModel
    {
        public string Name { get; set; } = string.Empty;



        public decimal AmounthPerMonth { get; set; }


        public int Months { get; set; }
    }
}
