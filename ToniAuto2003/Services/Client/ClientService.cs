using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Contracts.Client;
using ToniAuto2003.Data;

namespace ToniAuto2003.Services.Client
{
    public class ClientService : IClientInterface
    {
        private readonly ToniAutoDbContext _data;

        public ClientService(ToniAutoDbContext data)
        {
            _data = data;
        }

        public async Task<bool> CheckIfHeIsSalesMan(string userId)
        {
            return await _data.SalesMans.AnyAsync(x => x.UserId == userId);
        }

        public async Task Create(string userId,decimal amount)
        {
            var client = new Clients()
            {
                UserId = userId,
                Money = amount
            };

            await _data.Clients.AddAsync(client);
            await _data.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string userId)
        {
            return await _data.Clients.AnyAsync(c => c.UserId == userId);
        }

        public async  Task<int?> GetUserId(string userId)
        {
            return (await _data.Clients.FirstOrDefaultAsync(a => a.UserId == userId))?.Id;
        }
    }
}
