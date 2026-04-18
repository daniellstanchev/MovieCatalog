using Microsoft.AspNetCore.Mvc;
using MoviesCatalog.Data;
using Microsoft.EntityFrameworkCore;

namespace MoviesCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .Take(3)
                .ToListAsync();

            return View(movies);
        }
    }
}