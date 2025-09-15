using KeyHub.Market.data;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KeyHub.Market.Services.impl;

public class GameManagerService : IGameManagerService
{
private readonly ApplicationDbContext _appDbContext;

public GameManagerService(ApplicationDbContext appDbContext)
{
    _appDbContext = appDbContext;
}

public async Task<EntityEntry<Game>> AddGame(string title,Genre genre,decimal price,Platform platform,int stock,IFormFile imageFile,int discount=0)
{

   Task<(bool, string? fileName)> ImageUrl= UploadFile(imageFile);
   {



       if (ImageUrl.Result.fileName != null)
       {



           Game game = new Game()
           {
               Title = title,
               Genre = genre,
               Price = price,
               Discount = discount,
               Platform = platform,
               ImageUrl = ImageUrl.Result.fileName,
               Stock = stock
           };
           await _appDbContext.AddAsync(game);
           await _appDbContext.SaveChangesAsync();
           
       }
       
   }
  
    return null;

}






private async Task<(bool, string? fileName)> UploadFile(IFormFile? ufile)
{
    if (ufile != null && ufile.Length > 0)
    {
        string fileName = Guid.NewGuid() + Path.GetExtension(ufile.FileName);
        string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var filePath = Path.Combine(uploads, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await ufile.CopyToAsync(fileStream);
        }
         Console.WriteLine($"Uploaded {fileName}");
        return (true, fileName);
    }
    return (false, null);
}

    
}