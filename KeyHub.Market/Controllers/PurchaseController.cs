using KeyHub.Market.data;
using KeyHub.Market.Middlewares;
using KeyHub.Market.Models;
using KeyHub.Market.Models.ViewModels;
using KeyHub.Market.Services.impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace KeyHub.Market.Controllers;
    public class PurchaseController : Controller
    {   private readonly IHubContext<PurchaseNotificationHub> _hubContext;
        private readonly PurchaseService _purchaseService;
        private readonly UserManager<User> _userManager;


        public PurchaseController(IHubContext<PurchaseNotificationHub> hubContext, PurchaseService purchaseService, UserManager<User> userManager)
        {
            _hubContext = hubContext;
            _purchaseService = purchaseService;
            _userManager = userManager;
        }


        [HttpGet("Game/{id}")]
        public async Task<IActionResult> GameDetails(int id)
        {
            var game = await _context.Games.FindAsync(id);
           
            return View(game);
        }

        // POST: /Purchase/Buy/5
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
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return RedirectToAction("Home", "Home");
        }
        
        
        
        
        // PurchaseNotificationViewModel purchaseNotification=  new PurchaseNotificationViewModel()
        //       {    Username = user.UserName!,
        //           Title = game.Title, Price = game.Price, Discount = game.Discount, Platform = game.Platform,
        //           ImageUrl = game.ImageUrl
        //       };
        //       
        //       await _hubContext.Clients.All.SendAsync("ReceiveNotifications", purchaseNotification);
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        [HttpPost("/addMoney")]
        public async Task<IActionResult> AddMoney(decimal amount)
        {
            
            var user = await _userManager.GetUserAsync(User);

            user!.Balance += amount;
            await _userManager.UpdateAsync(user);

            return Redirect(Request.Headers["Referer"].ToString());
        

        }
        
    
 
    }
