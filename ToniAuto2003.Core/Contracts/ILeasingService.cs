using ToniAuto2003.Core.Enumerations;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;

namespace ToniAuto2003.Core.Contracts
{
    public interface ILeasingService
    {
        Task<int> CreateAsync(LeasingFormModel model, int agentId);

        Task<AllLeasingQueryModel> AllAsync(
            string? searchTerm = null,
            LeasingSorting sorting = LeasingSorting.Newest,
            int currentPage = 1,
            int leasingsperpage = 1);

        Task<bool> ExistsAsync(int id);

        Task<LeasingServiceModel> LeasingDetailsByIdAsync(int id);
    }
}
