using HouseRentingSystem.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Models.Leasing;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services
{
    public class LeasingService : ILeasingService
    {
        private readonly IRepository repository;

        public LeasingService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<int> CreateAsync(LeasingFormModel model, int agentId)
        {
            Leasing leasing = new Leasing()
            {
                Name=model.Name,
                AmounthPerMonth=model.AmounthPerMonth,
                Months=model.Months
            };

            await repository.AddAsync(leasing);
            await repository.SaveChangesAsync();

            return leasing.Id;
        }
    }
}
