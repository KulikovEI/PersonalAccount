using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Constants;
using PersonalAccount.Services.Cabinet;
using PersonalAccount.Utils;
using PersonalAccount.ViewModels;

namespace PersonalAccount.Controllers;

[Authorize(Roles = AccountRoleConstants.Teacher)]
public class TeacherCabinetController(ITeacherCabinetService cabinetService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var accountId = User.GetId();
        if (accountId == null) return Forbid();

        var groupsByDisciplines = await cabinetService.GetAllGroupsByDisciplinesAsync(accountId.Value);

        return View(new TeacherCabinetViewModel
        {
            DisciplineIdsOrder = groupsByDisciplines.Keys
                .OrderBy(discipline => discipline.Name)
                .Select(discipline => discipline.Id)
                .ToList(),
            Disciplines = groupsByDisciplines.Keys.ToDictionary(discipline => discipline.Id,
                discipline =>
                    new TeacherCabinetDisciplineViewModel
                    {
                        Name = discipline.Name,
                    }),
            Groups = groupsByDisciplines.ToDictionary(groups => groups.Key.Id,
                groups => groups.Value.Select(group => new TeacherCabinetGroupViewModel
                {
                    Name = group.Name,
                    ImageUrl = group.ImageUrl?.ToString()
                }).ToList())
        });
    }
}