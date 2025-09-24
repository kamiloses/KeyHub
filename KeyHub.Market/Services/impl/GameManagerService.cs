using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KeyHub.Market.Services.impl;

public class GameManagerService : IGameManagerService
{
private readonly ApplicationDbContext _appDbContext;
private readonly IWebHostEnvironment _env;


public GameManagerService(ApplicationDbContext appDbContext, IWebHostEnvironment env)
{
    _appDbContext = appDbContext;
    _env = env;
}

public async Task<Game> AddGame(string title, Genre genre, decimal price, Platform platform, int stock, IFormFile imageFile, int discount = 0)
{
    var imageFileName = await UploadFile(imageFile);
    if (imageFileName == null)
        throw new ArgumentException("Image file is required");

    var game = new Game
    {
        Title = title,
        Genre = genre,
        Price = price,
        Discount = discount,
        Platform = platform,
        ImageUrl = imageFileName,
        Stock = stock
    };
    try
    {
        await _appDbContext.AddAsync(game);
        await _appDbContext.SaveChangesAsync();
    }
    catch (Exception e)
    {
        throw new DatabaseSavingException("There was a problem saving the game", e);
    }
    

    return game;
}




private async Task<string?> UploadFile(IFormFile? ufile)
{
    if (ufile == null || ufile.Length == 0) return null;

    string fileName = Guid.NewGuid() + Path.GetExtension(ufile.FileName);
    string uploads = Path.Combine(_env.WebRootPath, "images", "games");
    Directory.CreateDirectory(uploads);

    var filePath = Path.Combine(uploads, fileName);
    await using var fileStream = new FileStream(filePath, FileMode.Create);
    await ufile.CopyToAsync(fileStream);

    Console.WriteLine($"Uploaded {fileName}");
    return fileName;
}
    
}