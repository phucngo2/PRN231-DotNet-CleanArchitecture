namespace PRN231.Application.Helpers;

public static class EnvironmentHelpers
{
    public static string GetJwtKey()
    {
        return Environment.GetEnvironmentVariable("JWT_KEY") ?? "qRHD98sXt4uTWtj2m0o0xRFRnUs8g9wb";
    }

    public static string GetSalt()
    {
        return Environment.GetEnvironmentVariable("SALT") ?? "cBLzPU3VJb4pzPnrOVNXfEmGWQavdNZn";
    }
}
