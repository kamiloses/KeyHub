using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;

public class HistoryController : Controller
{

    private readonly IHistoryService _historyService;
    private readonly UserManager<User> _userManager;
    private const int DefaultPageSize = 10;
    public HistoryController(IHistoryService historyService, UserManager<User> userManager)
    {
        _historyService = historyService;
        _userManager = userManager;
    }

    [HttpGet("/history")]
    // [Authorize] todo 
    public async Task<IActionResult> History()
    {
        var user = await _userManager.GetUserAsync(User);
        var purchases = await _historyService.GetUserPurchaseHistoryAsync(user!.Id);
        int totalCount = purchases.Count; 
        int totalPages = (int)Math.Ceiling(totalCount / (double)DefaultPageSize);
        
       HistoryViewModel historyViewModel= new HistoryViewModel() { Purchases = purchases, TotalPages = totalPages };
        
        return View(historyViewModel);
    }
}
