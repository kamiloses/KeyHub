using KeyHub.Market.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Register()
    {
        
       // await _authService.Register("admin1", "admin@gmail.com", "admin1+");
        return View("Register");
        
    }
    
    
    
    
    
}