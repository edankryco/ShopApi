using System.ComponentModel.DataAnnotations;

namespace SiteTask.Model;

public class User : IComparable<User>
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Pass { get; set; }
    public string ReplacePass { get; set; }
    public object Balans { get; set; }

    public User(string name, string mail, string pass,string replacePass, object balans)
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

        return other.Name == Name ? 0 : 1;
    }
}