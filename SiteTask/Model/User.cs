using System.ComponentModel.DataAnnotations;

namespace SiteTask.Model;

public class User : IComparable<User>
{
    public User(string name, int age, string mail, Password pass, Password replacePass, int balanc)
    {
        Name = name;
        Age = age;
        Mail = mail;
        Pass = pass;
        ReplacePass = replacePass;
        Balanc = balanc;
    }

    public string Name { get; set; }
    public int Age { get; set; }
    public string Mail { get; set; }
    public Password Pass { get; set; }
    public Password ReplacePass { get; set; }
    public int Balanc { get; set; }

    

    public int CompareTo(User? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (other.Name == Name)
        {
            return string.Compare(Mail, other.Mail, StringComparison.Ordinal);
        }

        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }
}