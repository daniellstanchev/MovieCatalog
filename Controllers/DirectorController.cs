using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Models;

namespace MoviesCatalog.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly AppDbContext _context;

        public DirectorsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Directors.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (director == null) return NotFound();

            return View(director);
        }
        public IActionResult Create()
        {
            ViewBag.Movies = _context.Movies.OrderBy(m => m.Title).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country")] Director director, List<int> selectedMovieIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(director);
                await _context.SaveChangesAsync();

                if (selectedMovieIds != null && selectedMovieIds.Any())
                {
                    foreach (var movieId in selectedMovieIds)
                    {
                        var movie = await _context.Movies.FindAsync(movieId);
                        if (movie != null)
                        {
                            movie.DirectorId = director.Id;
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Movies = _context.Movies.OrderBy(m => m.Title).ToList();
            return View(director);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (director == null) return NotFound();

            var allMovies = await _context.Movies.OrderBy(m => m.Title).ToListAsync();
            var selectedMovieIds = director.Movies?.Select(m => m.Id).ToList() ?? new List<int>();

            ViewBag.Movies = allMovies;
            ViewBag.SelectedMovieIds = selectedMovieIds;
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country")] Director director, List<int> selectedMovieIds)
        {
            if (id != director.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingDirector = await _context.Directors
                        .Include(d => d.Movies)
                        .FirstOrDefaultAsync(d => d.Id == id);

                    if (existingDirector != null)
                    {
                        if (existingDirector.Movies != null)
                        {
                            foreach (var movie in existingDirector.Movies)
                            {
                                movie.DirectorId = null;
                            }
                        }

                        existingDirector.Name = director.Name;
                        existingDirector.Country = director.Country;

                        if (selectedMovieIds != null && selectedMovieIds.Any())
                        {
                            foreach (var movieId in selectedMovieIds)
                            {
                                var movie = await _context.Movies.FindAsync(movieId);
                                if (movie != null)
                                {
                                    movie.DirectorId = existingDirector.Id;
                                }
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Movies = _context.Movies.OrderBy(m => m.Title).ToList();
            return View(director);
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.Id == id);
        }
    }
}