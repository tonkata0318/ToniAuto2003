using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Infrastructure.Data;

namespace System.Linq
{
    public static class IQuerableCarExtension
    {
        public static IQueryable<CarServiceModel> ProjectToCarServiceModel(this IQueryable<Car> cars)
        {
            return cars
                .Select(c => new CarServiceModel()
                {
                    Id = c.Id,
                    Year = c.Year,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    IsBuyed = c.RenterId != ""
                }) ;
        }
    }
}
