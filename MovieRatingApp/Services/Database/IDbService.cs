namespace MovieRatingApp.Services.Database
{
    public interface IDbService
    {
        public Task<List<T>> GetMoviesAsync<T>();
        
        Task UpdateMovieRatingAsync(string movieId, byte starsCount);
    }
}
