namespace PersonalAccount.ViewModels;

public class AdminCabinetStudentViewModel : ViewModel
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetTeacherViewModel : ViewModel
{
    public int AccountId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}

public class AdminCabinetViewModel : ViewModel
{
    public List<AdminCabinetTeacherViewModel> Teachers { get; set; } = [];
    public List<AdminCabinetStudentViewModel> Students { get; set; } = [];
}