namespace MovieRatingApp.Models.ViewDTOs
{
    public class UpdateMovieCommandInput
    {
        public string MovieId { get; set; }

        public byte StarsCount { get; set; }
    }
}
