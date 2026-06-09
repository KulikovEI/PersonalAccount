using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class StudentProfileRepo(AppDbContext context, IMapper<StudentProfileEntity, StudentProfileModel> mapper)
    : ProfileRepo<StudentProfileEntity, StudentProfileModel>(context, mapper, ctx => ctx.StudentProfiles),
        IStudentProfileRepo
{
    public  async Task UpdateGroupByAccountIdAsync(int accountId, int groupId)
    {
        var entity = await Table.AsNoTracking().FirstOrDefaultAsync(sp => sp.AccountId == accountId) ?? throw new KeyNotFoundException($"Профиль студента для аккаунта с ID {accountId} не найден.");
        await UpdateByIdAsync(entity.Id, entityToUpdate =>
        {
            if (groupId == -1) 
            {
                entityToUpdate.GroupId = null;
            }
            else
            {
                entityToUpdate.GroupId = groupId;
            }
        });
    }
}