using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories.Mappers;

public class AccountMapper : IMapper<AccountEntity, AccountModel>
{
    public AccountEntity ToEntity(AccountModel model) => new()
    {
        Id = model.Id,
        Email = model.Email,
        PasswordHash = model.PasswordHash,
        Role = model.Role 
    };

    public AccountModel ToModel(AccountEntity entity) => new()
    {
        Id = entity.Id,
        Email = entity.Email,
        PasswordHash = entity.PasswordHash,
        Role = entity.Role
    };
}