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
                Months=model.Months,
                AgentId=agentId
            };

            await repository.AddAsync(leasing);
            await repository.SaveChangesAsync();

            return leasing.Id;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await repository.AllReadOnly<Leasing>()
                .AnyAsync(l => l.Id == id);
                
        }

        public async Task<LeasingServiceModel?> GetLeasingFormModelByIdAsync(int id)
        {
            return await repository.AllReadOnly<Leasing>()
                .Where(l => l.Id == id)
                .Select(l => new LeasingServiceModel()
                {
                    Id=l.Id,
                    Name = l.Name,
                    AmountPerMonth = l.AmounthPerMonth,
                    Months = l.Months
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> HasAgentWithIdAsync(int leasingId, string userId)
        {
            return await repository.AllReadOnly<Leasing>()
                .AnyAsync(c => c.Id == leasingId && c.Agent.UserId == userId);
        }

        public async Task<LeasingServiceModel> LeasingDetailsByIdAsync(int id)
        {
            return await repository.AllReadOnly<Leasing>()
                .Where(c => c.Id == id)
                .Select(c => new LeasingServiceModel()
                {
                    Id= c.Id,
                    Name=c.Name,
                    AmountPerMonth=c.AmounthPerMonth,
                    Months=c.Months
                })
                .FirstAsync();
        }

        public async Task EditAsync(int leasingId, LeasingServiceModel model)
        {
            var leasing = await repository.GetByIdAsync<Leasing>(leasingId);

            if (leasing != null)
            {
                leasing.Name= model.Name;
                leasing.AmounthPerMonth = model.AmountPerMonth;
                leasing.Months = model.Months;
                leasing.Id= model.Id;
                await repository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int leasingId)
        {
            var leasing = await repository.AllReadOnly<Leasing>().FirstOrDefaultAsync(c => c.Id == leasingId);

            await repository.DeleteAsync<Leasing>(leasingId);
            await repository.SaveChangesAsync();
        }

    }
}
