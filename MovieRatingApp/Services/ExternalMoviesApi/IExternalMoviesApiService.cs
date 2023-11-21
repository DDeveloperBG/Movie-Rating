using MovieRatingApp.Models.DTOs;

namespace MovieRatingApp.Services.ExternalMoviesApi
{
    public interface IExternalMoviesApiService
    {
        public Task<List<MovieFromApiResult>> GetMoviesAsync(int count);

        public Task<List<MovieDetailsFromApiResult>> GetMoviesDetailsAsync(List<MovieFromApiResult> movies);
    }
}
