using AutoMapper;
using MovieRatingApp.Models.DbModels;
using MovieRatingApp.Models.DTOs;
using MovieRatingApp.Services.ExternalMoviesApi;
using SQLite;

namespace MovieRatingApp.Services.Database
{
    public class DbService : IDbService
    {
        private const string DB_NAME = "AppDB.db";
        private const int SEED_SIZE = 25;

        private readonly IExternalMoviesApiService moviesApiService;
        private readonly IMapper mapper;
        private readonly SQLiteAsyncConnection database;

        public DbService(IExternalMoviesApiService moviesApiService, IMapper mapper)
        {
            this.moviesApiService = moviesApiService;
            this.mapper = mapper;

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_NAME);
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<Movie>().Wait();
            database.CreateTableAsync<MovieGenre>().Wait();
        }

        public async Task<List<T>> GetMoviesAsync<T>()
        {
            if (await IsNotSeededAsync())
            {
                await SeedAsync();
            }

            var result = await database.Table<Movie>().ToListAsync();

            return mapper.Map<List<T>>(result);
        }

        public async Task UpdateMovieRatingAsync(string movieId, byte starsCount)
        {
            var movie = await database.Table<Movie>().FirstAsync(x => x.Id == movieId);

            movie.Rating = starsCount;
            movie.UserRated = true;

            await database.UpdateAsync(movie);
        }

        private async Task<bool> IsNotSeededAsync()
        {
            return (await database.Table<Movie>().CountAsync()) == 0;
        }

        private async Task SeedAsync()
        {
            var movies = await moviesApiService.GetMoviesAsync(SEED_SIZE);
            var moviesWithDetails = await moviesApiService.GetMoviesDetailsAsync(movies);

            var dbMovies = CreateMovies(moviesWithDetails);
            await database.InsertAllAsync(dbMovies);

            var genres = CreateMovieGeneres(moviesWithDetails);
            await database.InsertAllAsync(genres);
        }

        private static List<Movie> CreateMovies(List<MovieDetailsFromApiResult> moviesWithDetails)
        {
            List<Movie> movies = new();
            foreach (var movieWithDetails in moviesWithDetails)
            {
                Movie movie = new()
                {
                    Id = movieWithDetails.ApiId,
                    Name = movieWithDetails.Title,
                    Description = movieWithDetails.Description,
                    Rating = (byte)Math.Floor(movieWithDetails.Rating / 2),
                    ImageUrl = movieWithDetails.ImageUrl,
                    Year = movieWithDetails.Year,
                };

                movies.Add(movie);
            }

            return movies;
        }

        private static List<MovieGenre> CreateMovieGeneres(List<MovieDetailsFromApiResult> movies)
        {
            List<MovieGenre> movieGenres = new();
            foreach (var movie in movies)
            {
                foreach (var genre in movie.Genres)
                {
                    MovieGenre movieGenre = new() { MovieId = movie.ApiId, Genre = genre.Name };

                    movieGenres.Add(movieGenre);
                }
            }

            return movieGenres;
        }
    }
}
