namespace SiteTask.Model.GetModel;

public class AdminGet : IComparable<AdminGet>
{
    public AdminGet(object id,object login, object rang)
    {
        Id = id;
        Login = login;
        Rang = rang;
    }

    public object Id { get; set; }
    public object Login { get; set; }
    public object Rang { get; set; }
    
    
    public int CompareTo(AdminGet? other)
    {
        if (other == null)
        {
            return -1;
        }

        if (other.Rang == Rang)
        {
            return string.CompareOrdinal(Login.ToString(), other.Login.ToString());
        }

        return string.CompareOrdinal(Rang.ToString(), other.Rang.ToString());
    }
}