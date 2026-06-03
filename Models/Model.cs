namespace PersonalAccount.Models;

public abstract class Model
{
    public int Id { get; init; }

    public override bool Equals(object? obj) =>
        obj is Model model
        && Id == model.Id;

    public override int GetHashCode() => Id;
}