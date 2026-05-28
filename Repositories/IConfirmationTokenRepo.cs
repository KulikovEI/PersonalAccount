using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IConfirmationTokenRepo
{
    Task CreateAsync(ConfirmationTokenModel token);
    Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId);
    Task ConfirmByIdAsync(int id);
    Task<HashSet<int>> GetConfirmedAccountIdsAsync();
}