using ToniAuto2003.Core.Models.Car;
using ToniAuto2003.Core.Models.Leasing;

namespace ToniAuto2003.Core.Contracts
{
    public interface ILeasingService
    {
        Task<int> CreateAsync(LeasingFormModel model, int agentId);
    }
}
