using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Statistics;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;
        public StatisticService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<StatisticsServiceModel> TotalAsync()
        {
            int totalCars = await repository.AllReadOnly<Car>()
                .CountAsync();

            int totalBuyed = await repository.AllReadOnly<Car>()
                .Where(c => c.RenterId != "")
                .CountAsync();

            return new StatisticsServiceModel()
            {
                totalCars = totalCars,
                totalBuyed = totalBuyed
            };
        }
    }
}
