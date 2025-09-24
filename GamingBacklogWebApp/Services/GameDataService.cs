using GamingBacklogWebApp.Models;

namespace GamingBacklogWebApp.Services
{
    public interface IGameDataService
    {
        Task<List<GameSearchResult>> SearchGamesAsync(string query);
        Task<GameDetails?> GetGameDetailsAsync(string gameId);
    }

    public class GameSearchResult
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
    }

    public class GameDetails
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public List<string> Platforms { get; set; } = new();
    }

    // Mock implementation - replace with real IGDB API integration
    public class MockGameDataService : IGameDataService
    {
        private readonly List<GameDetails> _mockGames = new()
        {
            new GameDetails
            {
                Id = "zelda-botw",
                Title = "The Legend of Zelda: Breath of the Wild",
                Description = "Step into a world of discovery, exploration, and adventure in The Legend of Zelda: Breath of the Wild, a boundary-breaking new game in the acclaimed series.",
                ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1tmu.jpg",
                ReleaseDate = new DateTime(2017, 3, 3),
                Genre = "Action-Adventure",
                Platforms = new List<string> { "Nintendo Switch", "Wii U" }
            },
            new GameDetails
            {
                Id = "cyberpunk-2077",
                Title = "Cyberpunk 2077",
                Description = "Cyberpunk 2077 is an open-world, action-adventure story set in Night City, a megalopolis obsessed with power, glamour and body modification.",
                ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1wyy.jpg",
                ReleaseDate = new DateTime(2020, 12, 10),
                Genre = "RPG",
                Platforms = new List<string> { "PC", "PlayStation 4", "PlayStation 5", "Xbox One", "Xbox Series X/S" }
            },
            new GameDetails
            {
                Id = "elden-ring",
                Title = "Elden Ring",
                Description = "THE NEW FANTASY ACTION RPG. Rise, Tarnished, and be guided by grace to brandish the power of the Elden Ring and become an Elden Lord in the Lands Between.",
                ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co4jni.jpg",
                ReleaseDate = new DateTime(2022, 2, 25),
                Genre = "Action RPG",
                Platforms = new List<string> { "PC", "PlayStation 4", "PlayStation 5", "Xbox One", "Xbox Series X/S" }
            },
            new GameDetails
            {
                Id = "god-of-war",
                Title = "God of War",
                Description = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters.",
                ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1tmu.jpg",
                ReleaseDate = new DateTime(2018, 4, 20),
                Genre = "Action-Adventure",
                Platforms = new List<string> { "PlayStation 4", "PC" }
            },
            new GameDetails
            {
                Id = "hollow-knight",
                Title = "Hollow Knight",
                Description = "Forge your own path in Hollow Knight! An epic action adventure through a vast ruined kingdom of insects and heroes.",
                ImageUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1rgr.jpg",
                ReleaseDate = new DateTime(2017, 2, 24),
                Genre = "Metroidvania",
                Platforms = new List<string> { "PC", "Nintendo Switch", "PlayStation 4", "Xbox One" }
            }
        };

        public async Task<List<GameSearchResult>> SearchGamesAsync(string query)
        {
            await Task.Delay(100); // Simulate API delay

            var results = _mockGames
                .Where(g => g.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(g => new GameSearchResult
                {
                    Id = g.Id,
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    ReleaseDate = g.ReleaseDate,
                    Genre = g.Genre
                })
                .ToList();

            return results;
        }

        public async Task<GameDetails?> GetGameDetailsAsync(string gameId)
        {
            await Task.Delay(100); // Simulate API delay

            return _mockGames.FirstOrDefault(g => g.Id == gameId);
        }
    }
}