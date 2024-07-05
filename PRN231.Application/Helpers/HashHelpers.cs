using System.Security.Cryptography;
using System.Text;

namespace PRN231.Application.Helpers;

[Obsolete("Deprecated! Use the built in PasswordHasher instead!")]
public static class HashHelpers
{
    private static readonly string _salt = EnvironmentHelpers.GetSalt();
    private const int _keySize = 64;
    private const int _iterations = 350000;
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    [Obsolete("Deprecated! Use the built in PasswordHasher.HashPassword instead!")]
    public static string HashPassword(string password)
    {
        byte[] hashedBytes = GetHashStr(password);
        return Convert.ToHexString(hashedBytes);
    }

    [Obsolete("Deprecated! Use the built in PasswordHasher.VerifyHashedPassword instead!")]
    public static bool VerifyPassword(string password, string hash)
    {
        byte[] hashedBytes = GetHashStr(password);
        byte[] hashToCompare = Convert.FromHexString(hash);
        return CryptographicOperations.FixedTimeEquals(hashedBytes, hashToCompare);
    }

    private static byte[] GetHashStr(string str)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(str),
            Encoding.UTF8.GetBytes(_salt),
            _iterations,
            _hashAlgorithm,
            _keySize);
    }
}
