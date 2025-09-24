using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;

public class PurchaseHistoryController : Controller
{

    private readonly IPurchaseHistoryService _purchaseHistoryService;
    private readonly UserManager<User> _userManager;
    private const int DefaultPageSize = 5;

    public PurchaseHistoryController(IPurchaseHistoryService purchaseHistoryService, UserManager<User> userManager)
    {
        _purchaseHistoryService = purchaseHistoryService;
        _userManager = userManager;
    }

    [HttpGet("/history")]
    public async Task<IActionResult> History([FromQuery(Name = "CurrentPage")] int currentPage = 1)
    {

        var user = await _userManager.GetUserAsync(User);

        var (purchases, totalCount) = await _purchaseHistoryService
            .GetUserPurchaseHistoryAsync(user!.Id, currentPage, DefaultPageSize);

        int totalPages = (int)Math.Ceiling(totalCount / (double)DefaultPageSize);

        var historyViewModel = new HistoryViewModel()
        {
            Purchases = purchases,
            TotalPages = totalPages,
            CurrentPage = currentPage
        };

        return View(historyViewModel);
    }
}