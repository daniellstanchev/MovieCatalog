using System.Text.Json.Serialization;

namespace MoviesCatalog.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }

        [JsonIgnore]
        public Actor? Actor { get; set; }
    }
}