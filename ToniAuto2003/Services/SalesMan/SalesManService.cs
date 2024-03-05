using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Contracts.SalesMan;
using ToniAuto2003.Data;

namespace ToniAuto2003.Services.SalesMan
{
    public class SalesManService : ISalesManInterface
    {
        private readonly ToniAutoDbContext _data;

        public SalesManService(ToniAutoDbContext data)
        {
            _data = data;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var salesMan = new Data.SalesMan()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            await _data.SalesMans.AddAsync(salesMan);
            await _data.SaveChangesAsync();
        }

        public async Task<bool> ExistById(string userId)
        {
            return await _data.SalesMans.AnyAsync(c => c.UserId == userId);
        }

        public async Task<int?> GetUserId(string userId)
        {
            return (await _data.SalesMans.FirstOrDefaultAsync(a => a.UserId == userId))?.Id;
        }

        public async Task<bool> SalesManWithPhoneNumberExists(string phoneNumber)
        {
            return await _data.SalesMans.AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
