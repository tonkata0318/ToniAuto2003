using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Home;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<CarIndexServiceModel>> LastThreeCarsAsync();

        Task<IEnumerable<CarCategoryServiceModel>> AllCategoriesAsync();

        Task<IEnumerable<CarLeasingServiceModel>> AllLeasingsAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<bool> LeasingExistsAsync(int leasingId);

        Task<int> CreateAsync(CarFormModel model,int agentId);
    }
}
