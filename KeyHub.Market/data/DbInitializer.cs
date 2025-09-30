using KeyHub.Market.Models;
using Microsoft.AspNetCore.Identity;

namespace KeyHub.Market.data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context,UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Games.RemoveRange(context.Games);
            context.SaveChanges();
context.Games.AddRange(
    new Game { Title = "Grand Theft Auto San Andreas", Genre = Genre.Action, Price = 49.99m, Discount = 0, ImageUrl = "71NX7liCeBL._AC_SX342_SY445_QL70_ML2_.jpg", Platform = Platform.XboxLive, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Assassin's Creed Shadows", Genre = Genre.Action, Price = 69.99m, Discount = 20, ImageUrl = "assasins creed shadows p5.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Assassin's Creed Valhalla", Genre = Genre.Action, Price = 69.99m, Discount = 0, ImageUrl = "Assasins creed valhala ps5 .jpg", Platform = Platform.PSN, Stock = 10, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Bloodborne", Genre = Genre.Action, Price = 69.99m, Discount = 10, ImageUrl = "bloodborne.jpg", Platform = Platform.PSN, Stock = 5, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Cyberpunk 2077", Genre = Genre.RPG, Price = 59.99m, Discount = 0, ImageUrl = "Cyberpunk ps.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Dead by Daylight", Genre = Genre.Action, Price = 39.99m, Discount = 0, ImageUrl = "Dead by daylight ps.jpg", Platform = Platform.PSN, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Demon's Souls", Genre = Genre.Action, Price = 69.99m, Discount = 0, ImageUrl = "demon souls.jpg", Platform = Platform.PSN, Stock = 0, CreatedAt = DateTime.UtcNow }, // niedostępna
    new Game { Title = "EA SPORTS FC 25", Genre = Genre.Sports, Price = 59.99m, Discount = 5, ImageUrl = "EA SPORT FC 25.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Euro Truck Simulator 2", Genre = Genre.Simulation, Price = 29.99m, Discount = 0, ImageUrl = "Euro truck simulator pc.jpg", Platform = Platform.Steam, Stock = 10, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Far Cry Primal", Genre = Genre.Action, Price = 49.99m, Discount = 0, ImageUrl = "far cry primal ps.jpg", Platform = Platform.PSN, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Far Cry 4", Genre = Genre.Action, Price = 39.99m, Discount = 0, ImageUrl = "far cry 4 xbox.jpg", Platform = Platform.XboxLive, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Farming Simulator 25", Genre = Genre.Simulation, Price = 59.99m, Discount = 0, ImageUrl = "Farming simulator 25.jpg", Platform = Platform.PSN, Stock = 10, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Forza Horizon 5", Genre = Genre.Sports, Price = 59.99m, Discount = 0, ImageUrl = "Forza horizon xbox.jpg", Platform = Platform.XboxLive, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Grand Theft Auto V", Genre = Genre.Action, Price = 59.99m, Discount = 15, ImageUrl = "gta 5.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Hearts of Iron IV", Genre = Genre.Strategy, Price = 39.99m, Discount = 0, ImageUrl = "Hearts of iron 4 pc.jpg", Platform = Platform.Steam, Stock = 10, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Mafia: The Old Country", Genre = Genre.Action, Price = 59.99m, Discount = 0, ImageUrl = "mafia.jpg", Platform = Platform.PSN, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Minecraft", Genre = Genre.Adventure, Price = 49.99m, Discount = 0, ImageUrl = "minecraft.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Minecraft", Genre = Genre.Adventure, Price = 49.99m, Discount = 0, ImageUrl = "Minecraft Xbox one.jpg", Platform = Platform.XboxLive, Stock = 5, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Red Dead Redemption 2", Genre = Genre.Action, Price = 59.99m, Discount = 0, ImageUrl = "RDR2.jpg", Platform = Platform.PSN, Stock = 10, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Resident Evil 2", Genre = Genre.Action, Price = 49.99m, Discount = 0, ImageUrl = "Resident Evil 2 ps5.jpg", Platform = Platform.PSN, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Spider-Man: Miles Morales", Genre = Genre.Action, Price = 59.99m, Discount = 0, ImageUrl = "Spiderman Miles Morales ps5 .jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "The Last of Us", Genre = Genre.Action, Price = 69.99m, Discount = 0, ImageUrl = "the last of us ps5.jpg", Platform = Platform.PSN, Stock = 20, CreatedAt = DateTime.UtcNow },
    new Game { Title = "Watch Dogs 2", Genre = Genre.Action, Price = 49.99m, Discount = 0, ImageUrl = "watch dogs 2 ps.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
    new Game { Title = "The Witcher 3", Genre = Genre.RPG, Price = 59.99m, Discount = 0, ImageUrl = "witcher 3 xbox.jpg", Platform = Platform.XboxLive, Stock = 10, CreatedAt = DateTime.UtcNow }
);

context.SaveChanges();


//Creating new accounts
foreach (var user in userManager.Users.ToList())
{
    userManager.DeleteAsync(user).Wait();
}

foreach (var role in roleManager.Roles.ToList())
{
    roleManager.DeleteAsync(role).Wait();
}

roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
roleManager.CreateAsync(new IdentityRole("User")).Wait();

var adminUser = new User
{
    UserName = "admin",
    Email = "admin@gmail.com",
    EmailConfirmed = true,
    Balance = 1000
};
userManager.CreateAsync(adminUser, "Admin123!").Wait();
userManager.AddToRoleAsync(adminUser, "Admin").Wait();

var normalUser = new User
{
    UserName = "user",
    Email = "user@gmail.com",
    EmailConfirmed = true,
    Balance = 500
};
userManager.CreateAsync(normalUser, "User123!").Wait();
userManager.AddToRoleAsync(normalUser, "User").Wait();

        }
    }
}