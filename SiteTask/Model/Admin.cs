namespace SiteTask.Model;

public class Admin : IComparable<Admin>
{
    public Admin(int id, string login, int idAdmin, string rang)
    {
        Id = id;
        IdAdmin = idAdmin;
        Rang = rang;
    }

    public int Id { get; set; }
    public int IdAdmin { get; set; }
    public string Rang { get; set; }

    public int CompareTo(Admin? other)
    {
        if (other == null)
        {
            return -1;
        }

        if (other.Rang == Rang)
        {
            return string.CompareOrdinal(Id.ToString(), other.Id.ToString());
        }

        return string.CompareOrdinal(Rang, other.Rang);
    }
}