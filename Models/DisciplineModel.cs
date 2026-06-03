namespace PersonalAccount.Models;

public class DisciplineModel : Model
{
    public string Name { get; set; } = string.Empty;
    
    public override bool Equals(object? obj) =>
        obj is DisciplineModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}