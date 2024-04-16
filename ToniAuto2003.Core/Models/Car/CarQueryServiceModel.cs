namespace ToniAuto2003.Core.Models.Car
{
    public class CarQueryServiceModel
    {
        public int totalCarsCount { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();
    }
}
