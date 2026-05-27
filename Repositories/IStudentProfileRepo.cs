using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IStudentProfileRepo
{
    Task<StudentProfileModel?> GetByAccountIdAsync(int accountId);
    Task<List<StudentProfileModel>> GetAllAsync();
}