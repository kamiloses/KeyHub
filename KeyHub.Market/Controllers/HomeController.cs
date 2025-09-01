using Microsoft.AspNetCore.Mvc;

namespace KeyHub.Market.Controllers;
//todo zmien nazwe
public class HomeController : Controller
{
    [HttpGet("")] 
    public IActionResult Index()
    {
        return View();
    }
    
    
    
}