using ToniAuto2003.Core.Models.Client;
using ToniAuto2003.Infrastructure.Data;

namespace ToniAuto2003.Core.Contracts
{
    public interface IClientService
    {
        Task<bool> ExistByIdAsync(string userId);

        Task<bool> ExistAsAgentAsync(string userId);


        Task CreateAsync(string userId, double money);

        Task BuyAsync(int userId, double money);

        Task<ClientFormModel?> GetClientFormModelById(string id);

        Task SellAsync(int userId, double money);

    }
}
