namespace KeyHub.Market.Models;

public class Purchase
{
    public int Id { get; set; }

   
    public string UserId { get; set; }   
    public User User { get; set; }

 
    public int GameId { get; set; }
    public Game Game { get; set; }

   
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public decimal PurchasePrice { get; set; }
}
