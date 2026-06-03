using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Constants;
using PersonalAccount.Services.Account;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Services.Email;
using PersonalAccount.Types;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Admin)]
public class AdminCabinetController(
    IAdminCabinetService adminCabinetService,
    ITeacherCabinetService teacherCabinetService,
    IAccountService accountService,
    IEmailSenderService emailSenderService
) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accounts = await adminCabinetService.GetAllStudentAndTeacherAccountsAsync();
        var accountsDictionary = accounts.ToDictionary(account => account.Id);
        var studentProfiles = await adminCabinetService.GetAllStudentProfilesAsync();
        var teacherProfiles = await adminCabinetService.GetAllTeacherProfilesAsync();
        var groups = await adminCabinetService.GetAllGroupsAsync();
        var groupsDictionary = groups.ToDictionary(group => group.Id);

        return View(new AdminCabinetViewModel
        {
            Teachers = teacherProfiles
                .OrderBy(teacherProfile => teacherProfile.FullName)
                .Select(teacherProfile =>
                    new AdminCabinetTeacherViewModel
                    {
                        AccountId = teacherProfile.AccountId,
                        FullName = teacherProfile.FullName,
                        Email = accountsDictionary[teacherProfile.AccountId].Email,
                        PhotoUrl = teacherProfile.PhotoUrl?.ToString()
                    }).ToList(),
            Students = studentProfiles.Select(studentProfile => new AdminCabinetStudentViewModel
                {
                    FullName = studentProfile.FullName,
                    Email = accountsDictionary[studentProfile.AccountId].Email,
                    GroupName = groupsDictionary[studentProfile.GroupId].Name,
                    PhotoUrl = studentProfile.PhotoUrl?.ToString()
                }).OrderBy(student => student.GroupName)
                .ThenBy(student => student.FullName)
                .ToList(),
        });
    }

    [HttpGet]
    public IActionResult AddStudent()
    {
        return View(new AddStudentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddStudent(AddStudentViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var isAlreadyRegistered = await accountService.IsRegisteredAsync(model.Email);
        if (isAlreadyRegistered)
        {
            ModelState.AddModelError(string.Empty, "Email is already registered");
            return View(model);
        }

        var password = await accountService.RegisterAsync(model.Email, AccountRoles.Student);
        await adminCabinetService.AddStudentProfileAsync(model.Email, model.FullName);
        await emailSenderService.SendEmailAsync(model.ContactEmail, "Данные для входа в личный кабинет",
            $"""
             <head></head>
             <body>
             <p>{model.Email}</p>
             <p>{password}</p>
             </body>
             """);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AddTeacher()
    {
        return View(new AddTeacherViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTeacher(AddTeacherViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var isAlreadyRegistered = await accountService.IsRegisteredAsync(model.Email);
        if (isAlreadyRegistered)
        {
            ModelState.AddModelError(string.Empty, "Email is already registered");
            return View(model);
        }

        var password = await accountService.RegisterAsync(model.Email, AccountRoles.Teacher);
        await adminCabinetService.AddTeacherProfileAsync(model.Email, model.FullName);
        await emailSenderService.SendEmailAsync(model.ContactEmail, "Данные для входа в личный кабинет",
            $"""
             <head></head>
             <body>
             <p>{model.Email}</p>
             <p>{password}</p>
             </body>
             """);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditTeacher(int teacherAccountId)
    {
        var groupsByDisciplines = await teacherCabinetService.GetAllGroupsByDisciplinesAsync(teacherAccountId);
        var allGroups = await adminCabinetService.GetAllGroupsAsync();

        return View(new EditTeacherViewModel
        {
            TeacherAccountId = teacherAccountId,
            DisciplineIdsOrder = groupsByDisciplines.Keys
                .OrderBy(discipline => discipline.Name)
                .Select(discipline => discipline.Id)
                .ToList(),
            Disciplines = groupsByDisciplines.Keys.ToDictionary(discipline => discipline.Id,
                discipline =>
                    new EditTeacherDisciplineViewModel
                    {
                        Name = discipline.Name,
                        Id = discipline.Id,
                    }),
            Groups = groupsByDisciplines.ToDictionary(groups => groups.Key.Id,
                groups => groups.Value.Select(group => new EditTeacherGroupViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    ImageUrl = group.ImageUrl?.ToString()
                }).ToList()),
            AllGroups = allGroups.Where(group => group.Id != GroupConstants.NoGroupId).Select(group =>
                new EditTeacherGroupOptionViewModel
                {
                    Name = group.Name,
                    Id = group.Id,
                }).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditTeacher(int teacherAccountId, int disciplineId, int groupId)
    {
        await adminCabinetService.AddTeacherGroupDisciplineAsync(teacherAccountId, groupId, disciplineId);
        return RedirectToAction("EditTeacher", new { teacherAccountId });
    }
}