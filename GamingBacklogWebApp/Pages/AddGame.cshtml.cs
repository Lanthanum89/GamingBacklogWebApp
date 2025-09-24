using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GamingBacklogWebApp.Data;
using GamingBacklogWebApp.Models;
using GamingBacklogWebApp.Services;

namespace GamingBacklogWebApp.Pages
{
    public class AddGameModel : PageModel
    {
        private readonly GamingBacklogContext _context;
        private readonly IGameDataService _gameDataService;

        public AddGameModel(GamingBacklogContext context, IGameDataService gameDataService)
        {
            _context = context;
            _gameDataService = gameDataService;
        }

        [BindProperty]
        public Game Game { get; set; } = new();

        [BindProperty]
        public string? SearchQuery { get; set; }

        public List<GameSearchResult> SearchResults { get; set; } = new();

        [BindProperty]
        public string? SelectedGameId { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                SearchResults = await _gameDataService.SearchGamesAsync(SearchQuery);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSelectGameAsync()
        {
            if (!string.IsNullOrEmpty(SelectedGameId))
            {
                var gameDetails = await _gameDataService.GetGameDetailsAsync(SelectedGameId);
                if (gameDetails != null)
                {
                    Game.Title = gameDetails.Title;
                    Game.Description = gameDetails.Description;
                    Game.ImageUrl = gameDetails.ImageUrl;
                    Game.Genre = gameDetails.Genre;
                    Game.ReleaseDate = gameDetails.ReleaseDate;
                    Game.ExternalId = gameDetails.Id;
                    
                    // Set default platform if available
                    if (gameDetails.Platforms.Any())
                    {
                        Game.Platform = gameDetails.Platforms.First();
                    }
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Game.DateAdded = DateTime.Now;
            if (Game.Status == GameStatus.Completed && !Game.DateCompleted.HasValue)
            {
                Game.DateCompleted = DateTime.Now;
            }

            _context.Games.Add(Game);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Games");
        }
    }
}