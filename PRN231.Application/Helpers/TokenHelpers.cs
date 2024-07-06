namespace PRN231.Application.Helpers;

public static class TokenHelpers
{
    public static string GenerateRandomToken(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var token = new char[length];

        for (int i = 0; i < length; i++)
        {
            token[i] = chars[random.Next(chars.Length)];
        }

        return new string(token);
    }

    public static string GenerateGuid()
    {
        var guid = Guid.NewGuid().ToString();
        return guid;
    }
}
