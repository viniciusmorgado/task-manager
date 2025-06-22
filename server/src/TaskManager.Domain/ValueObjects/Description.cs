#nullable disable
namespace TaskManager.Domain.ValueObjects;

public class Description
{
    public string Value { get; }

    public Description(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > 500) 
            throw new ArgumentException("Description must be 500 characters or less.");
        Value = value?.Trim() ?? string.Empty;
    }

    public override bool Equals(object obj)
    {
        return obj is Description other && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string value) => new(value);
}
