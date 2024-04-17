using ToniAuto2003.Core.Models.Statistics;

namespace ToniAuto2003.Core.Contracts
{
    public interface IStatisticService
    {
        Task<StatisticsServiceModel> TotalAsync();
    }
}
