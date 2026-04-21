using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [StringLength(500)]
        [Display(Name = "Comment")]
        public string? Comment { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Display(Name = "Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}