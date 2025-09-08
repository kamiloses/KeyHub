using System.Text;
using System.Text.Encodings.Web;
using KeyHub.Market.Models;
using KeyHub.Market.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace KeyHub.Market.Controllers;

public class AuthController : Controller
{

    private readonly IAuthService _authService;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(IAuthService authService, UserManager<IdentityUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }

    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View("Login");

    }


    [HttpGet("/register")]
    public IActionResult Register()
    {


        return View();

    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser users = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await _userManager.CreateAsync(users, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Auth");
            }


        }

     return View(model);}
    
}