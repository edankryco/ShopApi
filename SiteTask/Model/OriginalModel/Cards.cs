namespace SiteTask.Model;

public class Cards
{
    public Cards(string name, byte[] img, int idUser, string description)
    {
        Name = name;
        Img = img;
        IdUser = idUser;
        Description = description;
    }
    
    public string Name { get; set; }
    public byte[] Img { get; set; }
    public int IdUser { get; set; }
    public string Description { get; set; }
}