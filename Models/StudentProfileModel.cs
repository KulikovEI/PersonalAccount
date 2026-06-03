using PersonalAccount.Constants;

namespace PersonalAccount.Models;

public class StudentProfileModel : ProfileModel
{
    public int GroupId { get; set; } = GroupConstants.NoGroupId;
    
    public override bool Equals(object? obj) =>
        obj is StudentProfileModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}