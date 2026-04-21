using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Controllers;
using MoviesCatalog.Data;
using MoviesCatalog.Models;
using Xunit;

namespace MoviesCatalog.Tests.Controllers
{
    public class ActorsControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Actors.Add(new Actor { Id = 1, Name = "Tom Hanks", BirthDate = new DateTime(1956, 7, 9) });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfActors()
        {
            var context = GetDbContext();
            var controller = new ActorsController(context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Actor>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new ActorsController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Actor>(viewResult.Model);
            Assert.Equal("Tom Hanks", model.Name);
        }

        [Fact]
        public async Task Details_InvalidId_ReturnsNotFound()
        {
            var context = GetDbContext();
            var controller = new ActorsController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}