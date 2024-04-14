using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Core.Contracts.Car;
using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services.Car
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<CarIndexServiceModel>> LastThreeCars()
        {
            return await repository
                .AllReadOnly<Infrastructure.Data.Car>()
                .OrderByDescending(c => c.Id)
                .Take(3)
                .Select(c => new CarIndexServiceModel()
                { 
                    Id= c.Id,
                    Model= c.Model,
                    ImageUrl= c.ImageUrl
                })
                .ToListAsync();


        }
    }
}
