using System.Security.Cryptography;
using System.Text;

namespace AutoCita.Api.Core.Security;

public sealed class PasswordHasher
{
    private const byte _keySize = 32;
    private const int _iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_keySize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize
        );

        return $"{Convert.ToBase64String(hash)}.{Convert.ToBase64String(salt)}";
    }

    public bool VerifyPassword(string password, string storedHash)
    {
        string[] parts = storedHash.Split('.', 2);

        if (parts.Length != 2)
        {
            return false;
        }

        byte[] hash = Convert.FromBase64String(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);

        byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            _iterations,
            _hashAlgorithm,
            _keySize
        );

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}
