namespace PersonalAccount.Models;

public abstract class ProfileModel : Model
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public Uri? PhotoUrl { get; set; }
    
    public override bool Equals(object? obj) =>
        obj is ProfileModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}