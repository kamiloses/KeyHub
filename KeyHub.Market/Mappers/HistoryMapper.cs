using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using KeyHub.Market.Models.ViewModels;

namespace KeyHub.Market.Mappers;

public class HistoryMapper
{
    public static PurchaseHistoryDto MapToHistoryDto(Purchase purchase)
    {
        return new PurchaseHistoryDto
        {
            Id = purchase.Id,
            UserId = purchase.UserId,
            User = purchase.User,
            GameId = purchase.GameId,
            Game = purchase.Game,
            PurchaseDate = purchase.PurchaseDate,
            PurchasePrice = purchase.PurchasePrice
        };


    }
}