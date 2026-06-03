using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Mappers;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public class TeacherGroupDisciplineRepo(
    AppDbContext context,
    IMapper<TeacherGroupDisciplineEntity, TeacherGroupDisciplineModel> mapper
) : Repo<TeacherGroupDisciplineEntity, TeacherGroupDisciplineModel>(context, mapper,
        ctx => ctx.TeacherGroupDisciplines),
    ITeacherGroupDisciplineRepo
{
    public async Task<List<TeacherGroupDisciplineModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId) =>
        await GetAllByAsync(entity => entity.TeacherAccountId == teacherAccountId);
}