namespace PRN231.Domain.Constants;

public static class EmailConstants
{
    public const string EMAIL_RESET_TOKEN_SUBJECT = "[PRN231] Reset Password";
    private const string EMAIL_RESET_TOKEN = "<a target=\"_blank\" href=\"{0}\">{1}</a>";
    public static readonly Func<string, string, string> GenerateResetTokenEmailBody =
        (string url, string content) => string.Format(EMAIL_RESET_TOKEN, url, content);
}
