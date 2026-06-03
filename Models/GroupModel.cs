namespace PersonalAccount.Models;

public class GroupModel : Model
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Uri? ImageUrl { get; set; }
    
    public override bool Equals(object? obj) =>
        obj is GroupModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}