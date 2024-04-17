using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                    Make=c.Make,
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
                .ProjectToCarServiceModel()
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

        public async Task<IEnumerable<CarServiceModel>> AllCarsByAgentIdAsync(int agentId)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.AgentId == agentId)
                .ProjectToCarServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> AllCarsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.RenterId == userId)
                .ProjectToCarServiceModel()
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c => c.Id == id);
        }

        public async Task<CarDetailsServiceModel> CarDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Car>()
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsServiceModel()
                {
                    Id = c.Id,
                    Year = c.Year,
                    Agent = new Models.Agent.AgentServiceModel()
                    {
                        FullName=$"{c.Agent.User.FirstName} {c.Agent.User.LastName}",
                        Email = c.Agent.User.Email,
                        PhoneNumber = c.Agent.PhoneNumber
                    },
                    Category = c.Category.Name,
                    Make = c.Make,
                    Model = c.Model,
                    ImageUrl = c.ImageUrl,
                    IsBuyed = c.RenterId != "",
                    Price = c.Price,
                })
                .FirstAsync();

        }

        public async Task EditAsync(int carId, CarFormModel model)
        {
            var car=await repository.GetByIdAsync<Car>(carId);

            if (car!=null)
            {
                car.Year = model.Year;
                car.Model=model.Model;
                car.Make = model.Make;
                car.CategoryId = model.CategoryId;
                car.ImageUrl=model.ImageUrl;
                car.LeasingId=model.LeasingId;
                car.Price = model.Price;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> HasAgentWithIdAsync(int carId, string userId)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c=>c.Id==carId && c.Agent.UserId == userId);
        }

        public async Task<CarFormModel?> GetCarFormModelByIdasync(int id)
        {
            var car= await repository.AllReadOnly<Car>()
                .Where(c => c.Id == id)
                .Select(c=>new CarFormModel()
                {
                    Year = c.Year,
                    Model = c.Model,
                    Make = c.Make,
                    ImageUrl = c.ImageUrl,
                    CategoryId=c.CategoryId,
                    LeasingId=c.LeasingId,
                    Price=c.Price
                }).FirstOrDefaultAsync();

            if (car!=null)

            {
                car.Categories = await AllCategoriesAsync();
                car.Leasings = await AllLeasingsAsync();
            }

            return car;
        }

        public async Task DeleteAsync(int carid)
        {
            var car = await repository.AllReadOnly<Car>().FirstOrDefaultAsync(c => c.Id == carid);

            await repository.DeleteAsync<Car>(carid);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> IsBuyedAsync(int carId)
        {
            bool result=false;
            var car = await repository.GetByIdAsync<Car>(carId);
            if (car!=null)
            {
                result =car.RenterId!="";
            }
            return result;
        }

        public async Task<bool> IsBuyedByUserWithIdAsync(int carId, string userId)
        {
            bool result = false;
            var car = await repository.GetByIdAsync<Car>(carId);
            if (car != null)
            {
                result = car.RenterId == userId;
            }
            return result;
        }

        public async Task BuyAsync(int id, string userId)
        {
            var car = await repository.GetByIdAsync<Car>(id);
            if (car != null)
            {
                car.RenterId = userId;
                await repository.SaveChangesAsync();
            }
        }

        public async Task SellAsync(int carid)
        {
            var car = await repository.GetByIdAsync<Car>(carid);
            if (car != null)
            {
                car.RenterId = "";
                await repository.SaveChangesAsync();
            }
        }
    }
}
