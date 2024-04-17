using ToniAuto2003.Core.Enumerations;
using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;
using ToniAuto2003.Infrastructure.Data;

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

        Task<bool> HasAgentWithIdAsync(int leasingId, string userId);

        Task<LeasingServiceModel?> GetLeasingFormModelByIdAsync(int id);

        Task EditAsync(int leasingId, LeasingServiceModel model);
    }
}
