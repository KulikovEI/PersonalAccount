using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet;

public interface ITeacherCabinetService
{
    Task<Dictionary<DisciplineModel, List<GroupModel>>> GetAllGroupsByDisciplinesAsync(
        int teacherAccountId
    );
}