using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Models;
using PersonalAccount.Services;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize]
public class CabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmation) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (User.IsInRole(AccountRole.Admin.ToString()))
        {
            return RedirectToAction("Admin");
        }

        if (User.IsInRole(AccountRole.Student.ToString()))
        {
            return RedirectToAction("Student");
        }

        return Forbid();
    }

    [HttpGet]
    [Authorize(Roles = "Student")] 
    public async Task<IActionResult> Student()
    {
        var accountId = User.GetId();
        var accountEmail = User.GetEmail();

        if (accountId is null || string.IsNullOrEmpty(accountEmail))
        {
            return RedirectToAction("Error", "Home");
        }

        var student = await cabinet.GetByAccountIdAsync(accountId.Value);
        if (student is null)
        {
            return RedirectToAction("Error", "Home");
        }

        var isEmailConfirmed = await confirmation.HasAnyConfirmedTokenAsync(accountId.Value);

        return View(new StudentCabinetViewModel
        {
            Email = accountEmail,
            FullName = student.FullName,
            GroupName = student.GroupName,
            PhotoUrl = student.PhotoUrl?.ToString(),
            IsEmailConfirmed = isEmailConfirmed
        });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Admin()
    {
        var accountEmail = User.GetEmail() ?? "Администратор";
        ViewData["AdminEmail"] = accountEmail;

        return View();
    }
}