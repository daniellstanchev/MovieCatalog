using System.ComponentModel.DataAnnotations;

namespace MoviesCatalog.Models
{
    public class Director
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Country")]
        [StringLength(50)]
        public string? Country { get; set; }
        public ICollection<Movie>? Movies { get; set; }
    }
}