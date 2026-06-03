namespace PersonalAccount.Models;

public class TeacherGroupDisciplineModel : Model
{
    public int DisciplineId { get; set; }
    public int GroupId { get; set; }
    public int TeacherAccountId { get; set; }
    
    public override bool Equals(object? obj) =>
        obj is TeacherGroupDisciplineModel 
        && base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}