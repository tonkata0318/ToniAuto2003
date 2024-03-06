namespace ToniAuto2003.Contracts.SalesMan
{
    public interface ISalesManInterface
    {
        Task<bool> SalesManWithPhoneNumberExists(string phoneNumber);

        Task Create(string userId, string phoneNumber);

        Task<int?> GetUserId(string userId);

        Task<bool> ExistById(string userId);

        Task<bool> CheckIfHeIsClient(string userId);
    }
}
