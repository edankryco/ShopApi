namespace SiteTask;

public interface IMyArgon
{
    public string Hash(string password, string salt);
    public string GeneratorSalt();
}

public class MyArgon2d : IMyArgon
{
    public string Hash(string password, string salt)
    {
        throw new NotImplementedException();
    }

    public string GeneratorSalt()
    {
        throw new NotImplementedException();
    }
}