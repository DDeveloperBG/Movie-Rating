namespace MovieRatingApp.Models.DTOs
{
    public class GetMoviesFromApiResult
    {
        public LinksObject Links { get; set; }

        public int Count { get; set; }

        public MovieFromApiResult[] Results { get; set; }

        public class LinksObject
        {
            public string Next { get; set; }
        }
    }
}
