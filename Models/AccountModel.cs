namespace PersonalAccount.Models;

public class AccountModel
{
    public int Id { get; set; }
    public AccountRole Role { get; set; } = AccountRole.Admin;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}