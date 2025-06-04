namespace PRN231.Domain.Common;

public static class Env
{
    public static readonly string? POSTGRES = Environment.GetEnvironmentVariable("POSTGRES") ?? null;
    public static readonly string? REDIS = Environment.GetEnvironmentVariable("REDIS") ?? null;
}
