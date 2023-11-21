using System.Text.Json.Serialization;

namespace MovieRatingApp.Models.DTOs
{
    public class MovieDetailsFromApiResult : MovieFromApiResult
    {
        public int Year { get; set; }

        public string Description { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("gen")]
        public List<MovieGenreObj> Genres { get; set; }

        public class MovieGenreObj
        {
            [JsonPropertyName("genre")]
            public string Name { get; set; }
        }
    }
}
