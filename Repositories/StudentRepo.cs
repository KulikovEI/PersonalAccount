using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories.Mappers;

namespace PersonalAccount.Repositories;

public class StudentRepo<T>(AppDbContext context, IMapper<StudentEntity, T> mapper) : IStudentRepo<T> where T : StudentModel
{
    private DbSet<StudentEntity> Students => context.Students;

    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id);

        return mapper.ToModel(entity);
    }

    public async Task<T?> GetByEmailAsync(string email)
    {
        var entity = await Students
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Email == email);
        return mapper.ToModel(entity);
    }

    public async Task UpdateByIdAsync(int id, StudentModel student)
    {
        var entity = await Students.FirstOrDefaultAsync(s => s.Id == id);

        if (entity != null)
        {
            entity.FullName = student.FullName;
            entity.GroupName = student.GroupName;
            entity.PhotoUrl = student.PhotoUrl?.ToString(); 
            await context.SaveChangesAsync();
        }
    }
}