using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
    public class PurchaseController : Controller
    {
     
        private readonly IPurchaseService _purchaseService;
        private readonly UserManager<User> _userManager;


        public PurchaseController(IPurchaseService purchaseService, UserManager<User> userManager)
        {
            _purchaseService = purchaseService;
            _userManager = userManager;
        }


        [HttpGet("Game/{id}")]
            public async Task<IActionResult> GameDetails(int id)
            {
                var game = await _purchaseService.GetGameByIdAsync(id);
                var user = User.Identity?.IsAuthenticated == true ? await _userManager.GetUserAsync(User) : null;

                var gamePurchaseViewModel = new GamePurchaseViewModel()
                {
                    Game = game,
                    UserBalance = user?.Balance ?? 0
                };

                return View(gamePurchaseViewModel);
            }
           

        [HttpPost("Game/{id}")]
        [Authorize]
        public async Task<IActionResult> BuyGame(int id)
        {
            
            User user = (await _userManager.GetUserAsync(User))!;
            
            try
            {
                await _purchaseService.BuyGameAsync(id, user);
                TempData["Success"] = "Purchase completed successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return RedirectToAction("GameDetails", "Purchase", new { id = id });
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
