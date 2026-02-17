using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .OrderByDescending(m => m.ReleaseYear)
                .ToListAsync();
            return View(movies);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewBag.AllActors = _context.Actors.OrderBy(a => a.Name).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseYear,Description,GenreId,SelectedActorIds")] Movie movie)
        {
            ModelState.Remove("SelectedActorIds");
            ModelState.Remove("Genre");
            ModelState.Remove("MovieActors");

            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                if (movie.SelectedActorIds != null && movie.SelectedActorIds.Any())
                {
                    foreach (var actorId in movie.SelectedActorIds)
                    {
                        _context.MovieActors.Add(new MovieActor
                        {
                            MovieId = movie.Id,
                            ActorId = actorId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = "Movie added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            ViewBag.AllActors = _context.Actors.OrderBy(a => a.Name).ToList();
            return View(movie);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            ViewBag.AllActors = await _context.Actors
                .OrderBy(a => a.Name)
                .ToListAsync();
            if (movie.MovieActors != null && movie.MovieActors.Any())
            {
                movie.SelectedActorIds = movie.MovieActors.Select(ma => ma.ActorId).ToList();
            }

            return View(movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseYear,Description,GenreId,SelectedActorIds")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            ModelState.Remove("SelectedActorIds");
            ModelState.Remove("Genre");
            ModelState.Remove("MovieActors");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovie = await _context.Movies
                        .Include(m => m.MovieActors)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingMovie == null)
                    {
                        return NotFound();
                    }
                    existingMovie.Title = movie.Title;
                    existingMovie.ReleaseYear = movie.ReleaseYear;
                    existingMovie.Description = movie.Description;
                    existingMovie.GenreId = movie.GenreId;
                    if (existingMovie.MovieActors != null && existingMovie.MovieActors.Any())
                    {
                        _context.MovieActors.RemoveRange(existingMovie.MovieActors);
                    }
                    if (movie.SelectedActorIds != null && movie.SelectedActorIds.Any())
                    {
                        foreach (var actorId in movie.SelectedActorIds)
                        {
                            _context.MovieActors.Add(new MovieActor
                            {
                                MovieId = id,
                                ActorId = actorId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Movie updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            ViewBag.AllActors = await _context.Actors.OrderBy(a => a.Name).ToListAsync();
            return View(movie);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.MovieActors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            if (movie.MovieActors != null && movie.MovieActors.Any())
            {
                _context.MovieActors.RemoveRange(movie.MovieActors);
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Movie deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}