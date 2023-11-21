using System.Text.Json.Serialization;

namespace MovieRatingApp.Models.DTOs
{
    public class GetMovieDetailsFromApiResult
    {
        [JsonPropertyName("results")]
        public MovieDetailsFromApiResult Result { get; set; }
    }
}
