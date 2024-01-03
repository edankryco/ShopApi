namespace SiteTask.Model.HashPasswordModel;

public interface IHash
{
    public int HashPass();
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
    
    public int HashPass()
    {
        var hash = 100;
        foreach (var data in Pass)
        {
            hash = ((hash << 5) + hash) ^ data;
        }

        return hash;
    }
}