using PersonalAccount.Models.Students;

namespace PersonalAccount.Services.Auth
{
    public interface IStudentAuthService
    {
        Task<StudentModel?> GetByIdAsync(int id);
        Task<StudentModel?> ValidateStudentAsync(string email, string password);
        Task SignInAsync(HttpContext ctx, StudentModel student);
        Task SignOutAsync(HttpContext ctx);
        Task UpdateByIdAsync(int id, StudentModel student);
    }
}