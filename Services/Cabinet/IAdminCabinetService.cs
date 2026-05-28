using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet
{
    public interface IAdminCabinetService
    {
        Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync();
        Task<List<StudentProfileModel>> GetAllStudentProfilesAsync();
        Task ConfirmStudentEmailAsync(int id);
    }
}
