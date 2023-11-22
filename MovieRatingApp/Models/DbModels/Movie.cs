using SQLite;

namespace MovieRatingApp.Models.DbModels
{
    [Table("movies")]
    public class Movie
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public byte Rating { get; set; }

        public int Year { get; set; }
        
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool UserRated { get; set; }
    }
}
