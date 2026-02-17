using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCatalog.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1888, 2026, ErrorMessage = "Release year must be between 1900 and 2026")]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [StringLength(1000)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public Genre? Genre { get; set; }

        [JsonIgnore]
        public ICollection<MovieActor>? MovieActors { get; set; }

        [NotMapped]
        [Display(Name = "Actors")]
        public List<int>? SelectedActorIds { get; set; }
    }
}