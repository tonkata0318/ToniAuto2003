namespace ToniAuto2003.Core.Contracts
{
    public interface IClientService
    {
        Task<bool> ExistByIdAsync(string userId);

        Task<bool> ExistAsAgentAsync(string userId);


        Task CreateAsync(string userId, double money);
    }
}
