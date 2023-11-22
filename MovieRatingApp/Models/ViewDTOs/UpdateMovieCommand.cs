namespace MovieRatingApp.Models.ViewDTOs
{
    public class UpdateMovieCommand
    {
        public string MovieId { get; set; }

        public byte StarsCount { get; set; }
    }
}
