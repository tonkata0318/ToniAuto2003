using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ToniAuto2003.Core.Enumerations;
using ToniAuto2003.Core.Models.Car;

namespace ToniAuto2003.Core.Models.Leasing
{
    public class AllLeasingQueryModel
    {
        public int LeasingsPerpage { get; } = 3;


        [Display(Name = "Search by text ")]
        public string SearchTerm { get; init; } = null!;

        public LeasingSorting Sorting { get; init; }

        public int currentPage { get; init; } = 1;

        public int TotalLeasingsCount { get; set; }

        public IEnumerable<LeasingServiceModel> Leasings { get; set; } = new List<LeasingServiceModel>();
    }
}
