using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamingBacklogWebApp.Data;
using GamingBacklogWebApp.Models;

namespace GamingBacklogWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly GamingBacklogContext _context;

    public IndexModel(ILogger<IndexModel> logger, GamingBacklogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public int TotalGames { get; set; }
    public int WishlistCount { get; set; }
    public int PlayingCount { get; set; }
    public int CompletedCount { get; set; }
    public List<Game> RecentlyAdded { get; set; } = new();
    public List<Game> RecentlyCompleted { get; set; } = new();

    public async Task OnGetAsync()
    {
        TotalGames = await _context.Games.CountAsync();
        WishlistCount = await _context.Games.CountAsync(g => g.Status == GameStatus.Wishlist);
        PlayingCount = await _context.Games.CountAsync(g => g.Status == GameStatus.Playing);
        CompletedCount = await _context.Games.CountAsync(g => g.Status == GameStatus.Completed);

        RecentlyAdded = await _context.Games
            .OrderByDescending(g => g.DateAdded)
            .Take(3)
            .ToListAsync();

        RecentlyCompleted = await _context.Games
            .Where(g => g.Status == GameStatus.Completed && g.DateCompleted.HasValue)
            .OrderByDescending(g => g.DateCompleted)
            .Take(3)
            .ToListAsync();
    }
}
