using System.ComponentModel.DataAnnotations;

namespace KeyHub.Market.Models.ViewModels;

public class GameManagerViewModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public Genre Genre { get; set; }

    [Required]
    [Range(0.20, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, 90)]
    public int Discount { get; set; }

    [Required]
    public Platform Platform { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Stock { get; set; }

    [Required]
    public IFormFile ImageFile { get; set; }  
}