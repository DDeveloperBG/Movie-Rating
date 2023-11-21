using SQLite;

namespace MovieRatingApp.Models.DbModels
{
    [Table("movie_genres")]
    public class MovieGenre
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string MovieId { get; set; }

        public string Genre { get; set; }
    }
}
