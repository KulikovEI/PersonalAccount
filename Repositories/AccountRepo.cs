using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;
using PersonalAccount.Types;

namespace PersonalAccount.Repositories;

public class AccountRepo(AppDbContext context, IMapper<AccountEntity, AccountModel> mapper)
    : Repo<AccountEntity, AccountModel>(context, mapper, ctx => ctx.Accounts), IAccountRepo
{
    public async Task<AccountModel?> GetByEmailAsync(string email) =>
        await GetByAsync(entity => entity.Email == email);

    public async Task<List<AccountModel>> GetAllByRoleAsync(AccountRoles role) =>
        await GetAllByAsync(entity => (entity.Role & role) != 0);
}

// 01101010
// 11011110
// 01001010

// 001 Admin
// 101 Admin | Student
// 001 != 0

// 001 Admin
// 110 Teacher | Student
// 000 == 0
