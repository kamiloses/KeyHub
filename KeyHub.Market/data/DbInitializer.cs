using KeyHub.Market.Models;

namespace KeyHub.Market.data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Games.Any())
                return;

            var games = new Game[100];

            string[] titles = new[]
            {
                "Grand Theft Auto V", "The Witcher 3", "Cyberpunk 2077", "Minecraft",
                "Halo Infinite", "FIFA 23", "Call of Duty: Modern Warfare", "Assassin's Creed Valhalla",
                "Red Dead Redemption 2", "Fortnite", "Overwatch", "Elden Ring", "Stardew Valley",
                "Rocket League", "DOOM Eternal", "Resident Evil Village", "Hades", "Among Us",
                "Animal Crossing", "Forza Horizon 5"
            };

            string[] genres = new[]
            {
                "Action", "Adventure", "RPG", "Shooter", "Sports", "Simulation", "Strategy"
            };

            Platform[] platforms = (Platform[])Enum.GetValues(typeof(Platform));

            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                games[i] = new Game
                {
                    Title = titles[random.Next(titles.Length)] + $" #{i + 1}",
                    Genre = genres[random.Next(genres.Length)],
                    Price = Math.Round((decimal)(10 + random.NextDouble() * 50), 2), // 10-60$
                    Discount = 0,
                    ImageUrl = "https://via.placeholder.com/150", // jedno zdjęcie dla wszystkich
                    Platform = platforms[random.Next(platforms.Length)],
                    Stock = random.Next(10, 101), // 10-100 sztuk
                    CreatedAt = DateTime.UtcNow
                };
            }

            context.Games.AddRange(games);
            context.SaveChanges();
        }
    }
}
