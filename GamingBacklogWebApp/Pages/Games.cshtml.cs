using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamingBacklogWebApp.Data;
using GamingBacklogWebApp.Models;

namespace GamingBacklogWebApp.Pages
{
    public class GamesModel : PageModel
    {
        private readonly GamingBacklogContext _context;

        public GamesModel(GamingBacklogContext context)
        {
            _context = context;
        }

        public List<Game> Games { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PlatformFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; } = "DateAdded";

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "desc";

        public List<string> Platforms { get; set; } = new();

        public async Task OnGetAsync()
        {
            var query = _context.Games.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(g => g.Title.Contains(SearchTerm) || 
                                        (g.Description != null && g.Description.Contains(SearchTerm)));
            }

            if (!string.IsNullOrEmpty(StatusFilter) && Enum.TryParse<GameStatus>(StatusFilter, out var status))
            {
                query = query.Where(g => g.Status == status);
            }

            if (!string.IsNullOrEmpty(PlatformFilter))
            {
                query = query.Where(g => g.Platform == PlatformFilter);
            }

            // Apply sorting
            query = SortBy switch
            {
                "Title" => SortOrder == "asc" ? query.OrderBy(g => g.Title) : query.OrderByDescending(g => g.Title),
                "Platform" => SortOrder == "asc" ? query.OrderBy(g => g.Platform) : query.OrderByDescending(g => g.Platform),
                "Status" => SortOrder == "asc" ? query.OrderBy(g => g.Status) : query.OrderByDescending(g => g.Status),
                "Rating" => SortOrder == "asc" ? query.OrderBy(g => g.Rating) : query.OrderByDescending(g => g.Rating),
                _ => SortOrder == "asc" ? query.OrderBy(g => g.DateAdded) : query.OrderByDescending(g => g.DateAdded),
            };

            Games = await query.ToListAsync();

            // Get available platforms for filter dropdown
            Platforms = await _context.Games
                .Where(g => !string.IsNullOrEmpty(g.Platform))
                .Select(g => g.Platform)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}