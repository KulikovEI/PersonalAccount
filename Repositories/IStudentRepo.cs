using PersonalAccount.Models.Students;

namespace PersonalAccount.Repositories
{
    public interface IStudentRepo<T> where T : StudentModel
    {
        public Task<T?> GetByIdAsync(int id);
        public Task<T?> GetByEmailAsync(string email);
        Task UpdateByIdAsync(int id, StudentModel student);
    }
}
