namespace PRN231.Domain.Enums;

public class UserRoles
{
    private UserRoles(string value) { Value = value; }
    public string Value { get; private set; }
    public override string ToString()
    {
        return Value;
    }

    public const string ADMIN = "Admin";
    public const string USER = "User";

    public static UserRoles Admin { get { return new UserRoles(ADMIN); } }
    public static UserRoles User { get { return new UserRoles(USER); } }

    public static UserRoles FromValue(string value)
    {
        return value switch
        {
            ADMIN => Admin,
            USER => User,
            _ => throw new ArgumentException($"Unknown User Role: {value}", nameof(value)),
        };
    }
}
