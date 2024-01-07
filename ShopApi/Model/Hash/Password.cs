namespace SiteTask.Model.Hash;

public interface IHash
{
    public string HashPass();
}

public class Password : IHash
{
    public Password(string pass)
    {
        Pass = pass;
    }

    public override string ToString()
    {
        return Pass;
    }

    public string Pass { get; set; }

    public string HashPass()
    {
        IMyArgon argon = new MyArgon2d();
        var salt = argon.GeneratorSalt(Pass);
        var hash = argon.Hash(Pass, salt);
        foreach (var data in hash)
        {
            return data.ToString();
        }

        return null;
    }
}