using System.ComponentModel.DataAnnotations;
using ToniAuto2003.Core.Contracts;
using static ToniAuto2003.Core.Constants.MessageConstants;
using static ToniAuto2003.Infrastructure.Constants.DataConstraints;

namespace ToniAuto2003.Core.Models.Car
{
    public class CarServiceModel : ICarModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public int Year { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Range(typeof(decimal), carsMinimalPrice, carsMaximumPrice, ErrorMessage = PriceOfCar)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Is Buyed")]
        public bool IsBuyed { get; set; }
    }
}