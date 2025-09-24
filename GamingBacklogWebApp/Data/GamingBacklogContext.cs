using Microsoft.EntityFrameworkCore;
using GamingBacklogWebApp.Models;

namespace GamingBacklogWebApp.Data
{
    public class GamingBacklogContext : DbContext
    {
        public GamingBacklogContext(DbContextOptions<GamingBacklogContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some sample data
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Status = GameStatus.Completed,
                    Rating = 9,
                    DateAdded = DateTime.Now.AddDays(-30),
                    DateCompleted = DateTime.Now.AddDays(-5),
                    Description = "An open-world action-adventure game",
                    Genre = "Action-Adventure"
                },
                new Game
                {
                    Id = 2,
                    Title = "Cyberpunk 2077",
                    Platform = "PC",
                    Status = GameStatus.Playing,
                    DateAdded = DateTime.Now.AddDays(-15),
                    Description = "A futuristic RPG set in Night City",
                    Genre = "RPG"
                },
                new Game
                {
                    Id = 3,
                    Title = "Elden Ring",
                    Platform = "PlayStation 5",
                    Status = GameStatus.Wishlist,
                    DateAdded = DateTime.Now.AddDays(-7),
                    Description = "A dark fantasy action RPG",
                    Genre = "Action RPG"
                }
            );
        }
    }
}