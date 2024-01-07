using System.Security.Cryptography;
using System.Text;

namespace SiteTask;

public interface IMyArgon
{
    public string Hash(string password, byte[] salt);
    public byte[] GeneratorSalt(string secret);
}

public class MyArgon2d : IMyArgon
{
    private int _iterations = 10;
    private int _memorySize = 65536;
    private readonly int saltSize = 16;
    private int _hashSize = 32;

    private byte[] _salt;

    public MyArgon2d()
    {
        _salt = new byte[saltSize];
    }

    public string Hash(string password, byte[] salt)
    {
        HMACSHA256 hmacsha256 = new HMACSHA256();
        
        byte[] passwordByte = Encoding.UTF8.GetBytes(password);
        byte[] saltedPasswordBytes = new byte[passwordByte.Length + salt.Length];
        Array.Copy(passwordByte, saltedPasswordBytes, passwordByte.Length);
        Array.Copy(_salt, 0, saltedPasswordBytes, passwordByte.Length, _salt.Length);

        return IterationHash(hmacsha256, saltedPasswordBytes);
    }

    private string IterationHash(HMACSHA256 hmacsha256, byte[] saltedByte)
    {
        for (int i = 0; i < _iterations; i++)
        {
            hmacsha256.Initialize();
            saltedByte = hmacsha256.ComputeHash(saltedByte);
        }

        return saltedByte.ToString();
    }

    [Obsolete("Obsolete")]
    public byte[] GeneratorSalt(string secret)
    {
        using var random = new RNGCryptoServiceProvider();
        random.GetBytes(_salt);

        return _salt;
    }
}