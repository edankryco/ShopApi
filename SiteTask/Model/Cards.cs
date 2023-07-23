namespace SiteTask.Model;

public class Cards
{
    public string Name { get; set; }
    public string Img { get; set; }
    public string Description { get; set; }

    public Cards(string name, string img, string description)
    {
        Name = name;
        Img = img;
        Description = description;
    }
}