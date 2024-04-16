using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Infrastructure.Data;
using ToniAuto2003.Core.Enumerations;

namespace ToniAuto2003.Core.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarIndexServiceModel>> LastThreeCarsAsync();

        Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync();

        Task<IEnumerable<CarLeasingServiceModel>> AllLeasingsAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<bool> LeasingExistsAsync(int leasingId);

        Task<int> CreateAsync(CarFormModel model, int agentId);

        Task<CarQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            CarsSorting sorting = CarsSorting.Newest,
            int currentPage = 1,
            int carsPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();

        Task<IEnumerable<CarServiceModel>> AllCarsByAgentIdAsync(int agentId);

        Task<IEnumerable<CarServiceModel>> AllCarsByUserIdAsync(string userId);

        Task<bool> ExistsAsync(int id);

        Task<CarDetailsServiceModel> CarDetailsByIdAsync(int id);
    }
}