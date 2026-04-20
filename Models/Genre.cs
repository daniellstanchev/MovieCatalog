using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCatalog.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Genre")]
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Movie>? Movies { get; set; }
    }
}