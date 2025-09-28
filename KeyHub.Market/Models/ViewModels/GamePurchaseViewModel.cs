namespace KeyHub.Market.Models.ViewModels;

public class GamePurchaseViewModel
{
    public required Game Game { get; set; }
    public decimal UserBalance { get; set; }
    public bool CanBuy => Game.Stock > 0 && UserBalance >= FinalPrice;
    public decimal FinalPrice => Math.Round(Game.Price * (1 - Game.Discount / 100m), 2);


    public string? BuyErrorMessage
    {
        get
        {
            if (Game.Stock <= 0) return "Game is out of stock.";
            if (UserBalance < FinalPrice) return "You do not have enough balance to buy this game.";
            return null;
        }
    }
}