using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories;

namespace PersonalAccount.Services.Auth
{
    public class StudentAuthService(IStudentRepo<StudentAuthModel> students, IPasswordHasher<StudentAuthModel> hasher)
        : IStudentAuthService
    {
        public async Task<StudentModel?> GetByIdAsync(int id)
        {
            return await students.GetByIdAsync(id);
        }

        public async Task<StudentModel?> ValidateStudentAsync(string email, string password)
        {
            var student = await students.GetByEmailAsync(email);
            if (student is null) return null;

            var result = hasher.VerifyHashedPassword(student, student.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) return null;
            return student.Clone() as StudentModel;
        }

        public async Task SignInAsync(HttpContext ctx, StudentModel student)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new(ClaimTypes.Name, student.FullName),
                new(ClaimTypes.Email, student.Email),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task SignOutAsync(HttpContext ctx) => await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        public async Task UpdateByIdAsync(int id, StudentModel student)
        {
            await students.UpdateByIdAsync(id, student);
        }
    }
}