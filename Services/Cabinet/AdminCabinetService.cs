using PersonalAccount.Models;
using PersonalAccount.Repositories;
using PersonalAccount.Services.Confirmation;

namespace PersonalAccount.Services.Cabinet
{
    public class AdminCabinetService(
        IAccountRepo accountRepo,
        IStudentProfileRepo studentProfileRepo, IConfirmationTokenService confirmationTokenService) : IAdminCabinetService
    {
        public async  Task ConfirmStudentEmailAsync(int id)
        {
            string token = await confirmationTokenService.GenerateTokenAsync(id);

            await confirmationTokenService.ValidateTokenAsync(id, token);
        }

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
