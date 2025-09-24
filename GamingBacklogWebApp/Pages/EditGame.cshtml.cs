using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamingBacklogWebApp.Data;
using GamingBacklogWebApp.Models;

namespace GamingBacklogWebApp.Pages
{
    public class EditGameModel : PageModel
    {
        private readonly GamingBacklogContext _context;

        public EditGameModel(GamingBacklogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            Game = game;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Update completion date based on status
            if (Game.Status == GameStatus.Completed && !Game.DateCompleted.HasValue)
            {
                Game.DateCompleted = DateTime.Now;
            }
            else if (Game.Status != GameStatus.Completed)
            {
                Game.DateCompleted = null;
            }

            _context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Games");
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}