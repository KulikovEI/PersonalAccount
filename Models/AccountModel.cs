using PersonalAccount.Types;

namespace PersonalAccount.Models;

public class AccountModel : Model
{
    public AccountRoles Role { get; set; } = AccountRoles.Student;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public override bool Equals(object? obj) =>
        obj is AccountModel
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}