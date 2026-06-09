using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IStudentProfileRepo : IProfileRepo<StudentProfileModel>
{
    Task UpdateGroupByAccountIdAsync(int accountId, int groupId);
}