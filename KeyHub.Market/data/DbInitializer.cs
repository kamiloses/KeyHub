using KeyHub.Market.Models;

namespace KeyHub.Market.data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Games.RemoveRange(context.Games);
            context.SaveChanges();

            context.Games.AddRange(
                new Game
                {
                    Title = "Farming Simulator 25", Genre = Genre.Simulation, Price = 59.99m, Discount = 0,
                    ImageUrl = "1e968ee4-6d5e-4ed7-bbf9-d6dbba642500.jpg", Platform = Platform.PSN, Stock = 50,
                    CreatedAt = DateTime.UtcNow
                },
                new Game
                {
                    Title = "Demon's Souls", Genre = Genre.Action, Price = 69.99m, Discount = 0,
                    ImageUrl = "1ef59ac1-fb49-4f3d-9a80-a71287ee67af.jpg", Platform = Platform.PSN, Stock = 50,
                    CreatedAt = DateTime.UtcNow
                },
                new Game { Title = "Bloodborne", Genre = Genre.Action, Price = 69.99m, Discount = 0, ImageUrl = "01d5c2ad-b9ae-465e-91de-653e47929102.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
                new Game { Title = "Grand Theft Auto V", Genre = Genre.Action, Price = 59.99m, Discount = 0, ImageUrl = "2c4f730e-37b3-49dd-8eed-93b5991060fc.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow },
                new Game { Title = "Minecraft", Genre = Genre.Action, Price = 49.99m, Discount = 0, ImageUrl = "03c5e356-5e9c-463a-9f53-a8ee9411ee4d.jpg", Platform = Platform.PSN, Stock = 50, CreatedAt = DateTime.UtcNow }
                );

            context.SaveChanges();
        }
    }
}