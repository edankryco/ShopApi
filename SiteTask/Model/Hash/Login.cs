namespace SiteTask.Model.Hash;

public class Login : IHash
{
    public Login(string login)
    {
        LoginUser = login;
    }

    public string LoginUser { get; set; }
    
    public string HashPass()
    {
        return "";
    }
}