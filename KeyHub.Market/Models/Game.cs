using KeyHub.Market.Controllers;

namespace KeyHub.Market.Models;

#pragma warning disable CS8618
public class Game
{
    
    public int Id { get; set; }
    public string Title { get; set; }
    public Genre Genre { get; set; } 
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public string ImageUrl { get; set; }
    public Platform Platform { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
