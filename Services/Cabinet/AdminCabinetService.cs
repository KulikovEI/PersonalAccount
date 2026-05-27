using PersonalAccount.Repositories;
using PersonalAccount.Models;

namespace PersonalAccount.Services.Cabinet
{
    public class AdminCabinetService(
        IAccountRepo accountRepo,
        IStudentProfileRepo studentProfileRepo) : IAdminCabinetService
    {
        public async Task<Dictionary<int, AccountModel>> GetAllStudentAccountsAsync()
        {
            var studentAccounts = await accountRepo.GetByRoleAsync(AccountRole.Student);

            return studentAccounts.ToDictionary(account => account.Id);
        }

        public async Task<List<StudentProfileModel>> GetAllStudentProfilesAsync()
        {
            return await studentProfileRepo.GetAllAsync();
        }
    }
}
