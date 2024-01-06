namespace SiteTask.Model;

public class Cards
{
    public Cards(string name, byte[] img, string login, string description)
    {
        Name = name;
        Img = img;
        Login = login;
        Description = description;
    }
    
    public string Name { get; set; }
    public byte[] Img { get; set; }
    public string Login { get; set; }
    public string Description { get; set; }
}