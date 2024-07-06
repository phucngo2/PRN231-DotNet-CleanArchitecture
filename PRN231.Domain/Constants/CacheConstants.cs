namespace PRN231.Domain.Constants;

public static class CacheConstants
{
    public static class Genres
    {
        public const string KEY = "PRN231_Genres";
        public const int EXPIRY = 300;
    }

    // I originally intended to use Redis for the reset token, but I decided to use the PostgreSQL database instead.
    public static class Tokens
    {
        public const string KEY = "PRN231_Tokens_{0}";
        public static readonly Func<int, string> GenerateKey = (id) => string.Format(KEY, id);
        public const int EXPIRY = 900;
    }
}