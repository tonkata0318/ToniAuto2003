namespace ToniAuto2003.Core.Models.Leasing
{
    public class LeasingDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal AmountPerMonth { get; set; }

        public int Months { get; set; }
    }
}
