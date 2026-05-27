using PersonalAccount.Models;

namespace PersonalAccount.Repositories
{
    public interface IAccountRepo
    {
        public Task<AccountModel?> GetByEmailAsync(string email);
        public Task<AccountModel?> GetByIdAsync(int id);
        Task<List<AccountModel>> GetByRoleAsync(AccountRole role);
    }
}
