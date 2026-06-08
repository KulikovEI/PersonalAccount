using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Types;
using PersonalAccount.Utils;

namespace PersonalAccount.Services.Cabinet;

public class AdminCabinetService(
    IAccountRepo accountRepo,
    IGroupRepo groupRepo,
    IStudentProfileRepo studentProfileRepo,
    ITeacherProfileRepo teacherProfileRepo,
    IDisciplineRepo disciplineRepo,
    ITeacherGroupDisciplineRepo teacherGroupDisciplineRepo
) : IAdminCabinetService
{
    public async Task<List<AccountModel>> GetAllStudentAndTeacherAccountsAsync() =>
        await accountRepo.GetAllByRoleAsync(AccountRoles.Student | AccountRoles.Teacher);

    public async Task<List<GroupModel>> GetAllGroupsAsync() => await groupRepo.GetAllAsync();

    public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync() => await studentProfileRepo.GetAllAsync();
    public async Task<List<TeacherProfileModel>> GetAllTeacherProfilesAsync() => await teacherProfileRepo.GetAllAsync();

    public async Task AddStudentProfileAsync(string email, string fullName) =>
        await AddProfileAsync(studentProfileRepo, email, fullName);

    public async Task AddTeacherProfileAsync(string email, string fullName) =>
        await AddProfileAsync(teacherProfileRepo, email, fullName);

    public async Task AddTeacherGroupDisciplineAsync(int teacherAccountId, int groupId, int disciplineId) =>
        await teacherGroupDisciplineRepo.AddAsync(new TeacherGroupDisciplineModel
        {
            DisciplineId = disciplineId,
            GroupId = groupId,
            TeacherAccountId = teacherAccountId
        });

    private async Task AddProfileAsync<TProfileModel>(
        IProfileRepo<TProfileModel> profileRepo,
        string email,
        string fullName
    ) where TProfileModel : ProfileModel, new()
    {
        var account = await accountRepo.GetByEmailAsync(email);
        if (account == null) return;

        await profileRepo.AddAsync(new TProfileModel
        {
            FullName = fullName,
            AccountId = account.Id
        });
    }

    public async Task AddGroupAsync(string name, string description, string? imageUrl)
    {
        var allGroups = await groupRepo.GetAllAsync();
        if (allGroups.Any(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Группа с названием «{name}» уже существует в системе.");
        }

        var newGroup = new GroupModel
        {
            Name = name.Trim(),
            Description = description.Trim(),
            ImageUrl = imageUrl?.ToUri()
        };

        await groupRepo.AddAsync(newGroup);
    }

    public async Task<List<DisciplineModel>> GetAllDisciplinesAsync() => await disciplineRepo.GetAllAsync();

    public async Task AddDisciplineAsync(string name)
    {
        var allDisciplines = await disciplineRepo.GetAllAsync();
        if (allDisciplines.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Дисциплина «{name}» уже существует в системе.");
        }

        var newDiscipline = new DisciplineModel
        {
            Name = name.Trim()
        };

        await disciplineRepo.AddAsync(newDiscipline);
    }

    public async Task ChangeStudentGroupAsync(int studentAccountId, int groupId)
    {
        if (groupId != -1) 
        {
            var targetGroup = await groupRepo.GetByIdAsync(groupId);
            if (targetGroup == null)
            {
                throw new InvalidOperationException("Выбранная учебная группа не существует в системе.");
            }
        }
        await studentProfileRepo.UpdateGroupByAccountIdAsync(studentAccountId, groupId);
    }
}