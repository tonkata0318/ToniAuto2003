namespace ToniAuto2003.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> ExistByIdAsync(string userId);

        Task<bool> ExistAsClientAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasCarsBuyedAsync(string userId);

        Task CreateAsync(string userId,string phoneNumber);
    }
}
