using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using ToniAuto2003.Core.Enumerations;

namespace ToniAuto2003.Core.Models.Car
{
    public class AllCarsQueryModel
    {
        public int CarsPerpage { get; }  = 3;

        public string Category { get; init; } = null!;

        [Display(Name = "Search by text ")]
        public string SearchTerm { get; init; } = null!;

        public CarsSorting Sorting { get; init; }

        public int currentPage { get; init; } = 1;

        public int TotalCarsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();
    }
}
