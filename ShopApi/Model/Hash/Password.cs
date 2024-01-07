namespace SiteTask.Model.Hash;

public interface IHash
{
    public string HashPass();
}

public class Password : IHash
{
    public Password(string pass, byte[] salt)
    {
        Pass = pass;
        Salt = salt;
    }

    public override string ToString()
    {
        return Pass;
    }

    public string Pass { get; set; }
    public byte[] Salt { get; set; }

    public string HashPass()
    {
        IMyArgon argon = new MyArgon2d();
        var salt = argon.GeneratorSalt(Pass);
        
        return argon.Hash(Pass, salt);
    }
}