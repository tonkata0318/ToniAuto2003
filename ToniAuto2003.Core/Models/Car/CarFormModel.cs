using System.ComponentModel.DataAnnotations;
using static ToniAuto2003.Core.Constants.MessageConstants;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;
using System.Xml.Linq;

namespace ToniAuto2003.Core.Models.Car
{
    public class CarFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        public int Year { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public string Make { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal), carsMinimalPrice, carsMaximumPrice, ErrorMessage = PriceOfCar)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CarCategoryServiceModel> Categories { get; set; } = new List<CarCategoryServiceModel>();

        [Display(Name = "Leasing")]
        public int LeasingId { get; set; }

        public IEnumerable<CarLeasingServiceModel> Leasings { get; set; } = new List<CarLeasingServiceModel>();
    }
}
