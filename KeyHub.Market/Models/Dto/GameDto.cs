namespace KeyHub.Market.Models.Dto;
#pragma warning disable CS8618

public class GameDto
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