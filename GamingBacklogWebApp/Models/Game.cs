using System.ComponentModel.DataAnnotations;

namespace GamingBacklogWebApp.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(50)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        public GameStatus Status { get; set; } = GameStatus.Wishlist;

        [Range(0, 10)]
        public int? Rating { get; set; }

        [StringLength(1000)]
        public string? Notes { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public DateTime? DateCompleted { get; set; }

        // Optional fields for external API data
        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? ImageUrl { get; set; }

        [StringLength(100)]
        public string? Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? ExternalId { get; set; }
    }

    public enum GameStatus
    {
        Wishlist,
        Playing,
        Completed,
        OnHold,
        Dropped
    }
}