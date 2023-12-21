using System.ComponentModel.DataAnnotations;

namespace SiteTask.Model;

public class User : IComparable<User>
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public Password Pass { get; set; }
    public Password ReplacePass { get; set; }
    public object Balans { get; set; }

    public User(string name, string mail, Password pass,Password replacePass, object balans)
    {
        Name = name;
        Mail = mail;
        Pass = pass;
        ReplacePass = replacePass;
        Balans = balans;
    }

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