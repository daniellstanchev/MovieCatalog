using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoviesCatalog.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [JsonIgnore]
        public ICollection<MovieActor>? MovieActors { get; set; }
    }
}