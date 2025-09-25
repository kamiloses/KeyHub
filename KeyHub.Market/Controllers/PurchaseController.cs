using KeyHub.Market.data;
using KeyHub.Market.Middlewares;
using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace KeyHub.Market.Controllers;
   [Authorize]
    public class PurchaseController : Controller
    {   private readonly IHubContext<PurchaseNotificationHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public PurchaseController(ApplicationDbContext context, UserManager<User> userManager, IHubContext<PurchaseNotificationHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        
        
        
        [HttpGet("Game/{id}")]
        public async Task<IActionResult> Buy(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();

            return View(game);
        }

        // POST: /Purchase/Buy/5
        [HttpPost("Game/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyConfirmed(int id)
        {
            Console.BackgroundColor= ConsoleColor.Red;
            Console.WriteLine("WYKONUJE SIE "+id);
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var game = await _context.Games.FindAsync(id);
            if (game == null || game.Stock <= 0)
            {
                TempData["Error"] = "Game not available";
                return RedirectToAction("Index", "SearchGame");
            }

            var price = game.Price - (game.Price * game.Discount / 100);

            if (user.Balance < price)
            {
                TempData["Error"] = "Not enough balance";
                return RedirectToAction("Buy", new { id = game.Id });
            }

            // Tworzymy zakup
            var purchase = new Purchase
            {
                UserId = user.Id,
                GameId = game.Id,
                PurchasePrice = price,
                PurchaseDate = DateTime.UtcNow
            };

            // Aktualizujemy bazę
            game.Stock -= 1;
            user.Balance -= price;

            _context.Purchases.Add(purchase);
            _context.Games.Update(game);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();




          PurchaseNotificationViewModel purchaseNotification=  new PurchaseNotificationViewModel()
            {    Username = user.UserName!,
                Title = game.Title, Price = game.Price, Discount = game.Discount, Platform = game.Platform,
                ImageUrl = game.ImageUrl
            };
            
            await _hubContext.Clients.All.SendAsync("ReceiveNotifications", purchaseNotification);
            
            TempData["Success"] = $"You purchased {game.Title}!";//todo jako cookie
            return RedirectToAction("Home", "Home");
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [HttpPost("/addMoney")]
        public async Task<IActionResult> AddMoney(decimal amount)
        {
            
            var user = await _userManager.GetUserAsync(User);

            user!.Balance += amount;
            await _userManager.UpdateAsync(user);

            return Redirect(Request.Headers["Referer"].ToString());
        

        }
        
    
 
    }
