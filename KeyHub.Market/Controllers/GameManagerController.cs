// using KeyHub.Market.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace KeyHub.Market.Controllers;
//
// public class GameManagerController : Controller
// {
//
//
//     [HttpPost]
//     public async Task<IActionResult> AddGame(
//         string title,
//         Genre genre,
//         decimal price,
//         int discount,
//         Platform platform,
//         int stock,
//         IFormFile imageFile)
//     {
//         if (string.IsNullOrEmpty(title) || price < 0 || stock < 0 || imageFile == null)
//         {
//             ModelState.AddModelError("", "Invalid data");
//             return View();
//         }
//
//     
//     
//     
//     
//     
// }