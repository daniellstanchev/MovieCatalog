using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Models;

namespace MoviesCatalog.Controllers
{
    public class GenresController : Controller
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genres
                .Include(g => g.Movies)
                .ToListAsync();
            return View(genres);
        }
        public async Task<IActionResult> Movies(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Movies)
                    .ThenInclude(m => m.MovieActors)
                        .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }
    }
}