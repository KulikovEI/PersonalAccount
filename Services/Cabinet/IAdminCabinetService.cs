using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface IAdminCabinetService
{
    Task<List<AccountModel>> GetAllStudentAndTeacherAccountsAsync();
    Task<List<GroupModel>> GetAllGroupsAsync();
    Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
    Task<List<TeacherProfileModel>> GetAllTeacherProfilesAsync();
    Task AddStudentProfileAsync(string email, string fullName);
    Task AddTeacherProfileAsync(string email, string fullName);
    Task AddTeacherGroupDisciplineAsync(int teacherAccountId, int groupId, int disciplineId);
}