namespace KeyHub.Market.Models.Dto;

public class PurchaseHistoryDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal PurchasePrice { get; set; }
}   