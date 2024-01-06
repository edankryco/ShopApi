using System.ComponentModel.DataAnnotations;
using SiteTask.Model.Hash;

namespace SiteTask.Model;

public class User
{
    public User(string login,string name, int age, string mail, Password pass, Password replacePass, int balanc)
    {
        Name = name;
        Login = login;
        Age = age;
        Mail = mail;
        Pass = pass;
        ReplacePass = replacePass;
        Balanc = balanc;
    }

    public string Login { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Mail { get; set; }
    public Password Pass { get; set; }
    public Password ReplacePass { get; set; }
    public int Balanc { get; set; }
}