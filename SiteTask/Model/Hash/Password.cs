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
        var hash = "";
        
        return hash;
    }
}