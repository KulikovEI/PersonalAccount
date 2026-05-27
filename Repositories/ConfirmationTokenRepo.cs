using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class ConfirmationTokenRepo(
    AppDbContext context,
    IMapper<ConfirmationTokenEntity, ConfirmationTokenModel> mapper) : IConfirmationTokenRepo
{
    private DbSet<ConfirmationTokenEntity> ConfirmationTokens => context.ConfirmationTokens;

    public async Task CreateAsync(ConfirmationTokenModel token)
    {
        await ConfirmationTokens.AddAsync(mapper.ToEntity(token));
        await context.SaveChangesAsync();
    }

    public async Task<List<ConfirmationTokenModel>> GetByAccountIdAsync(int accountId)
    {
        var entities = await ConfirmationTokens
            .AsNoTracking()
            .Where(t => t.AccountId == accountId)
            .ToListAsync();

        return [.. entities.Select(mapper.ToModel)];
    }

    public async Task ConfirmByIdAsync(int id)
    {
       var entity = await ConfirmationTokens.FindAsync(id) ?? throw new KeyNotFoundException();
       entity.ConfirmedAt = DateTime.UtcNow;
       await context.SaveChangesAsync();
    }
}