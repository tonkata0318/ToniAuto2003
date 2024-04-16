using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Core.Enumerations;
using ToniAuto2003.Core.Models.Car;
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

        public async Task<AllLeasingQueryModel> AllAsync(string? searchTerm = null, LeasingSorting sorting = LeasingSorting.Newest, int currentPage = 1, int leasingsperpage = 1)
        {
            var leasingstoShow = repository.AllReadOnly<Leasing>();


            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();
                leasingstoShow = leasingstoShow
                    .Where(c => (c.Name.ToLower().Contains(normalizedSearchTerm)));
            }

            leasingstoShow = sorting switch
            {
                LeasingSorting.Biggest => leasingstoShow
                .OrderByDescending(c => c.Months),
                LeasingSorting.Lowest => leasingstoShow
                .OrderBy(c=>c.Months),
                _ => leasingstoShow
                .OrderByDescending(c => c.Id)
            };

            var leasings = await leasingstoShow
                .Skip((currentPage - 1) * leasingsperpage)
                .Take(leasingsperpage)
                .Select(c => new LeasingServiceModel()
                {
                    Id=c.Id,
                    Name=c.Name,
                    AmountPerMonth=c.AmounthPerMonth,
                    Months=c.Months
                })
                .ToListAsync();

            int totalLeasings = await leasingstoShow.CountAsync();

            return new AllLeasingQueryModel()
            {
                Leasings = leasings,
                TotalLeasingsCount = totalLeasings,
            };
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
