namespace PersonalAccount.Types;

[Flags]
public enum AccountRoles
{
    Admin = 0x1,
    Teacher = 0x1 << 1,
    Student = 0x1 << 2,
}


// 0000000000000000001
// 0000000000000000010 = 0000000000000000001 << 1
// 0000000000000000100 = 0000000000000000001 << 2