using KeyHub.Market.Models;

namespace KeyHub.Market.data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // context.Games.RemoveRange(context.Games);
            // context.SaveChanges();
            //
            
            
            if (context.Games.Any())
                return;

            var games = new Game[100];

            string[] titles = new string[]
            {
                "Grand Theft Auto V", "The Witcher 3", "Cyberpunk 2077", "Minecraft",
                "Halo Infinite", "FIFA 23", "Call of Duty: Modern Warfare", "Assassin's Creed Valhalla",
                "Red Dead Redemption 2", "Fortnite", "Overwatch", "Elden Ring", "Stardew Valley",
                "Rocket League", "DOOM Eternal", "Resident Evil Village", "Hades", "Among Us",
                "Animal Crossing", "Forza Horizon 5"
            };

           

            Platform[] platforms = (Platform[])Enum.GetValues(typeof(Platform));
            var genres = (Genre[])Enum.GetValues(typeof(Genre)); 
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                games[i] = new Game
                {
                    Title = titles[random.Next(titles.Length)] + $" #{i + 1}",
                    Genre = genres[random.Next(genres.Length)],
                    Price = Math.Round((decimal)(10 + random.NextDouble() * 50), 2), // 10-60$
                    Discount = random.Next(5, 70),
                    ImageUrl = "https://m.media-amazon.com/images/I/81W+fYqjU0L._AC_SX569_.jpg", // jedno zdjęcie dla wszystkich
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
