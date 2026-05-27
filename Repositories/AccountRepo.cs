using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class AccountRepo(AppDbContext context, IMapper<AccountEntity, AccountModel> mapper) : IAccountRepo
{
    private DbSet<AccountEntity> Accounts => context.Accounts;

    public async Task<AccountModel?> GetByEmailAsync(string email)
    {
        var entity = await Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return entity == null ? null : mapper.ToModel(entity);
    }

    public async Task<AccountModel?> GetByIdAsync(int id)
    {
        var entity = await Accounts.FindAsync(id);
        return entity == null ? null : mapper.ToModel(entity);
    }

    public async Task<List<AccountModel>> GetByRoleAsync(AccountRole role)
    {
        var entities = await Accounts
            .AsNoTracking()
            .Where(account => account.Role == role)
            .ToListAsync();

        return [.. entities.Select(mapper.ToModel)];
    }
}