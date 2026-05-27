using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Db;

public class DbSeeder(
    AppDbContext context,
    IPasswordHasher<AccountModel> hasher,
    IMapper<AccountEntity, AccountModel> accountMapper)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        var hasStudents = await context.StudentProfiles.AnyAsync();
        if (hasStudents) return;

        var adminModel = new AccountModel
        {
            Email = "eugene020292@gmail.com",
            Role = AccountRole.Admin 
        };

        var adminEntity = accountMapper.ToEntity(adminModel);
        adminEntity.PasswordHash = hasher.HashPassword(adminModel, "admin123");

        await context.Accounts.AddAsync(adminEntity);
    }
}