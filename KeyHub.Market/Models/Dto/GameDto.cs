namespace KeyHub.Market.Dto;
#pragma warning disable CS8618

public class GameDto
{
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}