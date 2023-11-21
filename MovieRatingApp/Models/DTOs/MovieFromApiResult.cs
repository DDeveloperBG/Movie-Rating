using System.Text.Json.Serialization;

namespace MovieRatingApp.Models.DTOs
{
    public class MovieFromApiResult
    {
        [JsonPropertyName("imdb_id")]
        public string ApiId { get; set; }

        public string Title { get; set; }

        public decimal Rating { get; set; }
    }
}
