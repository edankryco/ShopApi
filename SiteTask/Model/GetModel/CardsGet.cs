namespace SiteTask.Model.GetModel;

public class CardsGet : IComparable<CardsGet>
{
    public CardsGet(object id, object name, object img, object login, object description)
    {
        Id = id;
        Name = name;
        Img = img;
        Login = login;
        Description = description;
    }

    public object Id { get; set; }
    public object Name { get; set; }
    public object Img { get; set; }
    public object Login { get; set; }
    public object Description { get; set; }
    
    public int CompareTo(CardsGet? other)
    {
        if (other == null)
        {
            return -1;
        }

        if (other.Id == Id)
        {
            return string.CompareOrdinal(Name.ToString(), other.Name.ToString());
        }

        return string.CompareOrdinal(Id.ToString(), other.Id.ToString());
    }
}