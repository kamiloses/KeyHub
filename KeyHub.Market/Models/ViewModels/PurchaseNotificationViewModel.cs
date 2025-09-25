namespace KeyHub.Market.Models.ViewModels;

public class PurchaseNotificationViewModel
{
    public string Username { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }
    public int Discount { get; set; }

    public string ImageUrl { get; set; }
    public Platform Platform { get; set; }
}