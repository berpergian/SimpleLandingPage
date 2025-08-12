using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTest.Models
{
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DirectorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<MovieDirection> MovieDirections { get; set; }
    }

    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ActorGender { get; set; }

        public IEnumerable<MovieCast> MovieCasts { get; set; }
    }

    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

        public int MovieYear { get; set; }

        public int MovieTime { get; set; }

        public string MovieLanguage { get; set; }

        public DateTime MovieDateTime { get; set; }

        public string MovieCountry { get; set; }

        public IEnumerable<MovieDirection> MovieDirections { get; set; }

        public IEnumerable<MovieCast> MovieCasts { get; set; }

        public IEnumerable<MovieGenre> MovieGenres { get; set; }

        public IEnumerable<Rating> MovieReviews { get; set; }
    }

    public class Reviewer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewerId { get; set; }

        public string ReviewerName { get; set; }

        public IEnumerable<Rating> MovieReviews { get; set; }
    }

    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        public string GenreTitle { get; set; }

        public IEnumerable<MovieGenre> MovieGenres { get; set; }
    }

    public class MovieDirection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int DirectorId { get; set; }

        public Movie Movie { get; set; }

        public Director Director { get; set; }
    }

    public class MovieCast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int ActorId { get; set; }

        public string Role { get; set; }

        public Movie Movie { get; set; }

        public Actor Actor { get; set; }
    }

    public class MovieGenre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int GenreId { get; set; }

        public Movie Movie { get; set; }

        public Genre Genre { get; set; }
    }

    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int ReviewerId { get; set; }

        public int ReviewStars { get; set; }

        public int NumberOfRating { get; set; } // from 0 to 10

        public Movie Movie { get; set; }

        public Reviewer Reviewer { get; set; }
    }
}
