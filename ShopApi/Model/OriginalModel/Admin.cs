namespace SiteTask.Model;

public class Admin
{
    public Admin(string login, int rang)
    {
        Login = login;
        Rang = rang;
    }

    public string Login { get; set; }
    public int Rang { get; set; }
}