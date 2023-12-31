namespace SiteTask.Model;

public class Admin : IComparable<Admin>
{
    public Admin(int idAdmin, int rang)
    {
        IdAdmin = idAdmin;
        Rang = rang;
    }

    public int IdAdmin { get; set; }
    public int Rang { get; set; }

    public int CompareTo(Admin? other)
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