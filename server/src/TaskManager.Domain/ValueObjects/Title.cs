#nullable disable
namespace TaskManager.Domain.ValueObjects;

public class Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Title is required");
        if (value.Length > 100) throw new ArgumentException("Title must be 100 characters or less.");

        Value = value.Trim();
    }

    public override bool Equals(object obj)
    {
        return obj is Title other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(Title title) => title.Value;
    public static implicit operator Title(string value) => new Title(value);
}
