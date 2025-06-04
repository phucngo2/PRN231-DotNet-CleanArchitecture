namespace PRN231.Domain.Extensions;

public static class StringExtensions
{
    public static bool EqualsIgnoreCase(this string str1, string str2)
    {
        return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
    }
}
