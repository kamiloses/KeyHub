using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;

namespace KeyHub.Market.Mappers;

public static class GameMapper
{
    public static GameDto ToDto(Game? game)
    {
        if (game == null) return null!;
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Genre = game.Genre,
            Price = game.Price,
            Discount = game.Discount,
            ImageUrl = game.ImageUrl,
            Platform = game.Platform,
            Stock = game.Stock,
            CreatedAt = game.CreatedAt
        };
    }

    public static List<GameDto> ToDtoList(IEnumerable<Game> games)
    {
        return games.Select(g => ToDto(g)).ToList();
    }
}