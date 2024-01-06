namespace SiteTask.Model.GetModel;

public class UsersGet : IComparable<UsersGet>
{
    public UsersGet(object login, object name, object age, object mail, object pass, object replacePass, object balanc)
    {
        Name = name;
        Login = login;
        Age = age;
        Mail = mail;
        Pass = pass;
        ReplacePass = replacePass;
        Balanc = balanc;
    }

    public object Login { get; set; }
    public object Name { get; set; }
    public object Age { get; set; }
    public object Mail { get; set; }
    public object Pass { get; set; }
    public object ReplacePass { get; set; }
    public object Balanc { get; set; }

    public int CompareTo(UsersGet? other)
    {
        if (other == null)
        {
            return -1;
        }

        if (other.Name == Name)
        {
            return string.Compare(Name.ToString(), other.Name.ToString(), StringComparison.Ordinal);
        }

        return string.Compare(Name.ToString(), other.Name.ToString(), StringComparison.Ordinal);
    }
}