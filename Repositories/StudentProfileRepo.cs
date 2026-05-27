using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class StudentProfileRepo(AppDbContext context, IMapper<StudentProfileEntity, StudentProfileModel> mapper)
    : IStudentProfileRepo
{
    private DbSet<StudentProfileEntity> StudentProfiles => context.StudentProfiles;

    public async Task<List<StudentProfileModel>> GetAllAsync()
    {
        var entities = await StudentProfiles
             .AsNoTracking()
             .ToListAsync();

        return [.. entities.Select(mapper.ToModel)];
    }

    public async Task<StudentProfileModel?> GetByAccountIdAsync(int accountId)
    {
        var entity = await StudentProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.AccountId == accountId);
        
        return entity == null ? null : mapper.ToModel(entity);
    }
}