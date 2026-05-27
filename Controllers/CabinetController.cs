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
public class CabinetController(IStudentCabinetService cabinet, IConfirmationTokenService confirmation, IAdminCabinetService adminCabinet) : Controller
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
    public async Task<IActionResult> Admin()
    {
        var accountsTask = adminCabinet.GetAllStudentAccountsAsync();
        var profilesTask = adminCabinet.GetAllStudentProfilesAsync();

        await Task.WhenAll(accountsTask, profilesTask);

        var accounts = accountsTask.Result;
        var profiles = profilesTask.Result;

        var adminStudentViews = new List<AdminCabinetStudentViewModel>();

        foreach (var profile in profiles)
        {
            if (accounts.ContainsKey(profile.AccountId))
            {
                adminStudentViews.Add(new AdminCabinetStudentViewModel
                {
                    FullName = profile.FullName,
                    GroupName = profile.GroupName,
                    PhotoUrl = profile.PhotoUrl?.ToString() 
                });
            }
        }

        return View(new AdminCabinetViewModel
        {
            Students = adminStudentViews
        });
    }
}