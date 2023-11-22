namespace MovieRatingApp.Models.ViewDTOs
{
    public class MainPageMovie
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public bool UserRated { get; set; }
    }
}
