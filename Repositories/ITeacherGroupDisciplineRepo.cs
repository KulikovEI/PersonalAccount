using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface ITeacherGroupDisciplineRepo : IRepo<TeacherGroupDisciplineModel>
{
    Task<List<TeacherGroupDisciplineModel>> GetAllByTeacherAccountIdAsync(int teacherAccountId);
}