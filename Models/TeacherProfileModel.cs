namespace PersonalAccount.Models;

public class TeacherProfileModel : ProfileModel
{
    public override bool Equals(object? obj) =>
        obj is TeacherProfileModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}