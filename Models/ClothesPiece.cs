namespace Api.Models;

public enum ClothesCategory
{
    Jeans,
    Shirts,
    Shoes,
    Hoodies
}

public class ClothesPiece (Guid id, string name, string desc, ClothesCategory category, string image)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Desc { get; set; } = desc;
    public ClothesCategory Category { get; set; } = category;
    public string Image { get; set; } = image;
    public string DownloadLink { get; set; } = $"http://localhost:8000{image}";
}