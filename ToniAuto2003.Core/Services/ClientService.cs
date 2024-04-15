using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository repository;

        public ClientService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task CreateAsync(string userId, double money)
        {
            await repository.AddAsync(new Clients()
            {
                UserId = userId,
                Money = money
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistAsAgentAsync(string userId)
        {
            return await repository.AllReadOnly<Agent>()
               .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> ExistByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Clients>()
                .AnyAsync(a => a.UserId == userId);
        }
    }
}
