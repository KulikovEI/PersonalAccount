namespace PersonalAccount.ViewModels
{
    public class AdminCabinetStudentViewModel
    {
        public int AccountId { get; set; }
        public string FullName { get; set; } = string.Empty;

        public string GroupName { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }

    public class AdminCabinetViewModel
    {
        public List<AdminCabinetStudentViewModel> Students { get; set; } = [];
    }
}
