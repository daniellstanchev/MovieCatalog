using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Models;

namespace MoviesCatalog.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
            return View(movies);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }
        public IActionResult Create()
        {
            ViewData["GenreId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Genres, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseYear,Description,GenreId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            ViewData["GenreId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseYear,Description,GenreId")] Movie movie)
        {
            if (id != movie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            return View(movie);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null) _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}