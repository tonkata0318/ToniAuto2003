using HouseRentingSystem.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Core.Contracts;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository repository;

        public AgentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Agent()
            { 
                UserId= userId,
                PhoneNumber= phoneNumber
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistAsClientAsync(string userId)
        {
            return await repository.AllReadOnly<Clients>()
               .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> ExistByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserHasCarsBuyedAsync(string userId)
        {
            return await repository.AllReadOnly<Car>()
                .AnyAsync(c => c.RenterId == userId);

        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Agent>()
                .AnyAsync(a=>a.PhoneNumber== phoneNumber);
                
        }
    }
}
