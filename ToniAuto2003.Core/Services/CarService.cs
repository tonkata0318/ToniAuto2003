using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Enumerations;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<CarIndexServiceModel>> LastThreeCarsAsync()
        {
            return await repository
                .AllReadOnly<Car>()
                .OrderByDescending(c => c.Id)
                .Take(3)
                .Select(c => new CarIndexServiceModel()
                {
                    Id = c.Id,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c => new CarCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

        }

        public async Task<IEnumerable<CarLeasingServiceModel>> AllLeasingsAsync()
        {
            return await repository.AllReadOnly<Leasing>()
               .Select(c => new CarLeasingServiceModel()
               {
                   Id = c.Id,
                   Name = c.Name
               }).ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<Category>()
                .AnyAsync(c => c.Id == categoryId);

        }

        public async Task<int> CreateAsync(CarFormModel model, int agentId)
        {
            Car car = new Car()
            {
                Year = model.Year,
                Make = model.Make,
                Model = model.Model,
                Price = model.Price,
                AgentId = agentId,
                CategoryId = model.CategoryId,
                LeasingId = model.LeasingId,
                ImageUrl = model.ImageUrl,
            };

            await repository.AddAsync(car);
            await repository.SaveChangesAsync();

            return car.Id;
        }
        public async Task<bool> LeasingExistsAsync(int leasingId)
        {
            return await repository.AllReadOnly<Leasing>()
               .AnyAsync(c => c.Id == leasingId);
        }

        public async Task<CarQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            CarsSorting sorting = CarsSorting.Newest,
            int currentPage = 1,
            int carsPerPage = 1)
        {
            var carsToShow = repository.AllReadOnly<Car>();

            if (category!=null)
            {
                carsToShow = carsToShow
                    .Where(c => c.Category.Name == category);
            }

            if (searchTerm != null)
            {
                string normalizedSearchTerm=searchTerm.ToLower();
                carsToShow = carsToShow
                    .Where(c => (c.Make.ToLower().Contains(normalizedSearchTerm)) ||
                    (c.Model.ToLower().Contains(normalizedSearchTerm)));
            }

            carsToShow = sorting switch
            {
                CarsSorting.Price => carsToShow
                .OrderByDescending(c => c.Price),
                CarsSorting.NotBuyed=> carsToShow
                .OrderBy(c=>c.RenterId==null)
                .ThenByDescending(c=>c.Id),
                _ => carsToShow
                .OrderByDescending(c=>c.Id)
            };

            var cars = await carsToShow
                .Skip((currentPage - 1) * carsPerPage)
                .Take(carsPerPage)
                .Select(c => new CarServiceModel()
                { 
                    Id = c.Id,
                    Year = c.Year,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price,
                    IsBuyed=c.RenterId!=null
                })
                .ToListAsync();

            int totalCars=await carsToShow.CountAsync();

            return new CarQueryServiceModel()
            {
                Cars = cars,
                totalCarsCount = totalCars,
            };
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<Category>()
                .Select(c=>c.Name)
                .Distinct()
                .ToListAsync();
        }

    }
}
