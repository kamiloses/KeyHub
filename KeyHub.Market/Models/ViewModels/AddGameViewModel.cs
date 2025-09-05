namespace KeyHub.Market.Models.ViewModels;

public class AddGameViewModel
{
    public string Title { get; set; }
    public List<Genre> SelectedGenres { get; set; } = new();
    public decimal Price { get; set; }
    public int Discount { get; set; } // w %
    public Platform Platform { get; set; }
    public int Stock { get; set; }
}