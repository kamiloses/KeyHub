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

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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
        if (!ModelState.IsValid)
        {
           
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"{kv.Key}: {err.ErrorMessage}");
                }
            }
            
            return View(model);
        }

        return RedirectToAction("Login");
    }

}