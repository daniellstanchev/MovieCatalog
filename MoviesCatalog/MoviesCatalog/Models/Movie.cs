using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието е задължително")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1888, 2026)]
        public int ReleaseYear { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        // Връзка с жанр
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}