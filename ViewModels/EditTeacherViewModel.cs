using System.Collections;

namespace PersonalAccount.ViewModels;

public class EditTeacherGroupViewModel : CabinetGroupViewModel
{
    public int Id { get; set; }
}

public class EditTeacherDisciplineViewModel : ViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class EditTeacherGroupOptionViewModel : ViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class EditTeacherViewModel
{
    public int TeacherAccountId { get; set; }
    public List<int> DisciplineIdsOrder { get; set; } = [];
    public Dictionary<int, EditTeacherDisciplineViewModel> Disciplines { get; set; } = [];
    public Dictionary<int, List<EditTeacherGroupViewModel>> Groups { get; set; } = [];
    public List<EditTeacherGroupOptionViewModel> AllGroups { get; set; } = [];
}