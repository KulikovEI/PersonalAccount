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
    Task AddGroupAsync(string name, string description, string? imageUrl);
    Task<List<DisciplineModel>> GetAllDisciplinesAsync();
    Task AddDisciplineAsync(string name);
    Task ChangeStudentGroupAsync(int studentAccountId, int groupId);
    Task DeleteGroupAsync(int groupId);
    Task DeleteDisciplineAsync(int disciplineId);
    Task DeleteStudentAsync(int studentAccountId);
    Task DeleteTeacherAsync(int teacherAccountId);

}