namespace ToniAuto2003.Contracts.Client
{
    public interface IClientInterface
    {
        Task Create(string userId,decimal amount);

        Task<int?> GetUserId(string userId);

        Task<bool> ExistById(string userId);
    }
}
