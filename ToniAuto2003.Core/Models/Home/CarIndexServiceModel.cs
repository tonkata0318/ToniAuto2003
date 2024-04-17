using ToniAuto2003.Core.Contracts;

namespace ToniAuto2003.Core.Models.Home
{
    public class CarIndexServiceModel : ICarModel
    {
        public int Id { get; set; }

        public string Model { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public string Make { get  ; set ; } = string.Empty;
    }
}
