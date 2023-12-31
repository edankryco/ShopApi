namespace SiteTask.Model.GetModel;

public class AdminGet : IComparable<AdminGet>
{
    public AdminGet(object idAdmin, object rang)
    {
        IdAdmin = idAdmin;
        Rang = rang;
    }

    public object IdAdmin { get; set; }
    public object Rang { get; set; }
    
    
    public int CompareTo(AdminGet? other)
    {
        if (other == null)
        {
            return -1;
        }

        if (other.Rang == Rang)
        {
            return string.CompareOrdinal(IdAdmin.ToString(), other.IdAdmin.ToString());
        }

        return string.CompareOrdinal(Rang.ToString(), other.Rang.ToString());
    }
}