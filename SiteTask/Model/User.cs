namespace SiteTask.Model;

public class User
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Pass { get; set; }
    public string Replace_Pass { get; set; }
    public int Balans { get; set; }

    public User(string name, string mail, string pass,string replacePass, int balans)
    {
        Name = name;
        Mail = mail;
        Pass = pass;
        Replace_Pass = replacePass;
        Balans = balans;
    }
}