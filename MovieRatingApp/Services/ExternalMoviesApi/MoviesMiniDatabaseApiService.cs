using MovieRatingApp.Models.DTOs;
using MovieRatingApp.Models.Http;

namespace MovieRatingApp.Services.ExternalMoviesApi
{
    public class MoviesMiniDatabaseApiService : IExternalMoviesApiService
    {
        private const string ApiURL = "https://moviesminidatabase.p.rapidapi.com";
        private const string ApiKeyHeader = "X-RapidAPI-Key";
        private const string ApiKey = "98e404e6a3msh1d21a114f0d8de4p103dd3jsnd8012b16960f";

        private readonly HttpHelperService httpService;

        public MoviesMiniDatabaseApiService(HttpHelperService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<MovieFromApiResult>> GetMoviesAsync(int count)
        {
            const string API_PATH = "movie/order/byRating";

            List<MovieFromApiResult> result = new();
            var responseObj = await httpService.MakeApiGetRequestAsync<GetMoviesFromApiResult>(
                $"{ApiURL}/{API_PATH}", ApiKeyHeader, ApiKey);
            result.AddRange(responseObj.Results);

            int ceiling = Math.Min(count, responseObj.Count);

            while (result.Count < ceiling)
            {
                responseObj = await httpService.MakeApiGetRequestAsync<GetMoviesFromApiResult>(
                    responseObj.Links.Next, ApiKeyHeader, ApiKey);
                result.AddRange(responseObj.Results);
            }

            int diff = result.Count - ceiling;
            if (diff > 0)
            {
                // We remove from the bottom as those movies have smaller rating so it's better to remove them
                result.RemoveRange(result.Count - diff, diff);
            }

            return result;
        }

        public async Task<List<MovieDetailsFromApiResult>> GetMoviesDetailsAsync(List<MovieFromApiResult> movies)
        {
            const string API_PATH = "movie/id";

            List<MovieDetailsFromApiResult> result = new(movies.Count);

            foreach (var movie in movies)
            {
                var responseObj = await httpService.MakeApiGetRequestAsync<GetMovieDetailsFromApiResult>(
                    $"{ApiURL}/{API_PATH}/{movie.ApiId}", ApiKeyHeader, ApiKey);
                
                result.Add(responseObj.Result);
            }

            return result;
        }
    }
}
