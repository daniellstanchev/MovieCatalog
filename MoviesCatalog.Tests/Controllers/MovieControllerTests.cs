using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Controllers;
using MoviesCatalog.Data;
using MoviesCatalog.Models;
using Xunit;

namespace MoviesCatalog.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Genres.Add(new Genre { Id = 1, Name = "Action" });
            context.Movies.Add(new Movie { Id = 1, Title = "Test Movie", ReleaseYear = 2024, GenreId = 1 });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfMovies()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<X.PagedList.IPagedList<Movie>>(viewResult.Model);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Movie>(viewResult.Model);
            Assert.Equal("Test Movie", model.Title);
        }

        [Fact]
        public async Task Details_InvalidId_ReturnsNotFound()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);
            var newMovie = new Movie { Title = "New Movie", ReleaseYear = 2025, GenreId = 1 };

            var result = await controller.Create(newMovie);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Edit_Get_ValidId_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Movie>(viewResult.Model);
            Assert.Equal("Test Movie", model.Title);
        }

        [Fact]
        public async Task Delete_Get_ValidId_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new MoviesController(context);

            var result = await controller.Delete(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Movie>(viewResult.Model);
            Assert.Equal("Test Movie", model.Title);
        }
    }
}