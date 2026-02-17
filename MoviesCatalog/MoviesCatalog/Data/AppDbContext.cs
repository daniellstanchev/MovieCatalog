using Microsoft.EntityFrameworkCore;
using MoviesCatalog.Models;

namespace MoviesCatalog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<Movie>().Ignore(m => m.SelectedActorIds);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Sci-Fi" },
                new Genre { Id = 5, Name = "Horror" }
            );

            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Tom Cruise", BirthDate = new DateTime(1962, 7, 3) },
                new Actor { Id = 2, Name = "Brad Pitt", BirthDate = new DateTime(1963, 12, 18) },
                new Actor { Id = 3, Name = "Leonardo DiCaprio", BirthDate = new DateTime(1974, 11, 11) },
                new Actor { Id = 4, Name = "Scarlett Johansson", BirthDate = new DateTime(1984, 11, 22) },
                new Actor { Id = 5, Name = "Denzel Washington", BirthDate = new DateTime(1954, 12, 28) }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, Description = "A thief who steals corporate secrets through dream-sharing technology.", GenreId = 4 },
                new Movie { Id = 2, Title = "The Dark Knight", ReleaseYear = 2008, Description = "Batman fights the Joker.", GenreId = 1 },
                new Movie { Id = 3, Title = "Pulp Fiction", ReleaseYear = 1994, Description = "Various stories of criminal Los Angeles.", GenreId = 3 },
                new Movie { Id = 4, Title = "The Matrix", ReleaseYear = 1999, Description = "A hacker discovers reality is a simulation.", GenreId = 4 },
                new Movie { Id = 5, Title = "Forrest Gump", ReleaseYear = 1994, Description = "The story of a man's extraordinary life.", GenreId = 3 }
            );

            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 3 }, 
                new MovieActor { MovieId = 1, ActorId = 4 }, 
                new MovieActor { MovieId = 2, ActorId = 1 }, 
                new MovieActor { MovieId = 2, ActorId = 2 }, 
                new MovieActor { MovieId = 3, ActorId = 2 }, 
                new MovieActor { MovieId = 3, ActorId = 5 }, 
                new MovieActor { MovieId = 4, ActorId = 4 }, 
                new MovieActor { MovieId = 4, ActorId = 3 }, 
                new MovieActor { MovieId = 5, ActorId = 3 }  
            );
        }
    }
}