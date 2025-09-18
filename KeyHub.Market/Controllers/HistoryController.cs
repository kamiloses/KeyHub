using KeyHub.Market.data;
using KeyHub.Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Controllers;

public class HistoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public HistoryController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: /History
    [HttpGet("/history")]
    public async Task<IActionResult> Purchases()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Challenge(); // jeśli niezalogowany

        var purchases = await _context.Purchases
            .Where(p => p.UserId == user.Id)
            .Include(p => p.Game)
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync();

        return View(purchases);
    }
}
