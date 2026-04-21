using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Data;
using MoviesCatalog.Models;

namespace MoviesCatalog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public AdminController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public IActionResult TestDashboard()
        {
            return View("~/Areas/Admin/Views/Admin/Dashboard.cshtml");
        }

        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Admin/Index.cshtml");
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalMovies = await _context.Movies.CountAsync();
            ViewBag.TotalActors = await _context.Actors.CountAsync();
            ViewBag.TotalGenres = await _context.Genres.CountAsync();
            ViewBag.TotalUsers = await _userManager.Users.CountAsync();
            ViewBag.RecentMovies = await _context.Movies
                .OrderByDescending(m => m.ReleaseYear)
                .Take(5)
                .ToListAsync();

            return View("~/Areas/Admin/Views/Admin/Dashboard.cshtml");
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                userRoles[user.Id] = await _userManager.GetRolesAsync(user);
            }

            ViewBag.UserRoles = userRoles;
            return View("~/Areas/Admin/Views/Admin/ManageUsers.cshtml", users);
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                TempData["Message"] = $"{user.Email} is now an Admin!";
            }
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                TempData["Message"] = $"{user.Email} is no longer an Admin.";
            }
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.Email != "admin@movies.com")
            {
                await _userManager.DeleteAsync(user);
                TempData["Message"] = $"User {user.Email} was deleted.";
            }
            return RedirectToAction("ManageUsers");
        }
    }
}