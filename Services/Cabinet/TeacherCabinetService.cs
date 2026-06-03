using PersonalAccount.Models;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Cabinet;

public class TeacherCabinetService(
    IGroupRepo groupRepo,
    ITeacherGroupDisciplineRepo teacherGroupDisciplineRepo,
    IDisciplineRepo disciplineRepo
) : ITeacherCabinetService
{
    public async Task<Dictionary<DisciplineModel, List<GroupModel>>> GetAllGroupsByDisciplinesAsync(
        int teacherAccountId
    )
    {
        var groupsByDisciplines = new Dictionary<DisciplineModel, List<GroupModel>>();
        var teacherGroupDisciplines = await teacherGroupDisciplineRepo.GetAllByTeacherAccountIdAsync(teacherAccountId);
        foreach (var teacherGroupDiscipline in teacherGroupDisciplines)
        {
            var discipline = await disciplineRepo.GetByIdAsync(teacherGroupDiscipline.DisciplineId);
            var group = await groupRepo.GetByIdAsync(teacherGroupDiscipline.GroupId);
            if (discipline == null || group == null) throw new KeyNotFoundException();
            if (!groupsByDisciplines.TryGetValue(discipline, out var groups))
                groupsByDisciplines[discipline] = [group];
            else
                groups.Add(group);
        }

        return groupsByDisciplines;
    }
}