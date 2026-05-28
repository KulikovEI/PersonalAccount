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
        var confirmedIdsTask = confirmation.GetConfirmedAccountIdsAsync();

        await Task.WhenAll(accountsTask, profilesTask, confirmedIdsTask);

        var accounts = accountsTask.Result;
        var profiles = profilesTask.Result;
        var confirmedIds = confirmedIdsTask.Result;

        var adminStudentViews = new List<AdminCabinetStudentViewModel>();

        foreach (var profile in profiles)
        {
            if (accounts.TryGetValue(profile.AccountId, out var account))
            {
                adminStudentViews.Add(new AdminCabinetStudentViewModel
                {
                    AccountId = profile.AccountId, 
                    FullName = profile.FullName,
                    GroupName = profile.GroupName,
                    PhotoUrl = profile.PhotoUrl?.ToString(),
                    IsEmailConfirmed = confirmedIds.Contains(profile.AccountId) 
                });
            }
        }

        return View(new AdminCabinetViewModel { Students = adminStudentViews });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmStudentEmail(int id)
    {
        if (id <= 0) return RedirectToAction("Error", "Home");

        await adminCabinet.ConfirmStudentEmailAsync(id);

        return RedirectToAction("Admin");
    }
}