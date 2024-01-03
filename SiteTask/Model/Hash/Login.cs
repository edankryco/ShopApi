namespace SiteTask.Model.HashPasswordModel;

public class Login : IHash
{
    public Login(string login)
    {
        LoginUser = login;
    }

    public string LoginUser { get; set; }
    
    public int HashPass()
    {
        return 0;
    }
}