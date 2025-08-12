using CodeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeTest.Configurations.Startup
{
    public class DatabaseHostService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseHostService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => DatabaseSeeding(), cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void DatabaseSeeding()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            context.Database.Migrate();

            var isDoingSeed = SeedData(context);
            if (isDoingSeed)
            {
                int seedingSuccess = context.SaveChanges();
                if (seedingSuccess > 0)
                {
                    Console.WriteLine($"SEEDING COMPLETE");
                }
                else
                {
                    Console.WriteLine($"SEEDING ERROR : Something went wrong when plating seed!");
                }
            }
        }

        private bool SeedData(DatabaseContext context)
        {
            var isSeeding = false;
            var random = new Random();

            // --- Genres ---
            if (!context.Genres.Any())
            {
                var genres = new[]
                {
                    new Genre { GenreTitle = "Action" },
                    new Genre { GenreTitle = "Drama" },
                    new Genre { GenreTitle = "Comedy" },
                    new Genre { GenreTitle = "Romance" },
                    new Genre { GenreTitle = "Sci-Fi" },
                    new Genre { GenreTitle = "Horror" },
                    new Genre { GenreTitle = "Thriller" },
                    new Genre { GenreTitle = "Adventure" },
                    new Genre { GenreTitle = "Fantasy" },
                    new Genre { GenreTitle = "Documentary" }
                };
                context.Genres.AddRange(genres);
                isSeeding = true;
            }

            // --- Movies ---
            if (!context.Movies.Any())
            {
                var movies = Enumerable.Range(1, 10).Select(i => new Movie
                {
                    MovieTitle = $"Movie {i}",
                    MovieYear = 2000 + i,
                    MovieTime = random.Next(80, 180), // minutes
                    MovieLanguage = "English",
                    MovieCountry = "USA",
                    MovieDateTime = DateTime.UtcNow.AddDays(-i)
                }).ToList();
                context.Movies.AddRange(movies);
                isSeeding = true;
            }

            // --- Actors ---
            if (!context.Actors.Any())
            {
                var actors = Enumerable.Range(1, 10).Select(i => new Actor
                {
                    FirstName = $"ActorFirst{i}",
                    LastName = $"ActorLast{i}",
                    ActorGender = (i % 2 == 0) ? "Male" : "Female"
                }).ToList();
                context.Actors.AddRange(actors);
                isSeeding = true;
            }

            // --- Directors ---
            if (!context.Directors.Any())
            {
                var directors = Enumerable.Range(1, 5).Select(i => new Director
                {
                    FirstName = $"DirectorFirst{i}",
                    LastName = $"DirectorLast{i}"
                }).ToList();
                context.Directors.AddRange(directors);
                isSeeding = true;
            }

            // --- Reviewers ---
            if (!context.Reviewers.Any())
            {
                var reviewers = Enumerable.Range(1, 5).Select(i => new Reviewer
                {
                    ReviewerName = $"Reviewer {i}"
                }).ToList();
                context.Reviewers.AddRange(reviewers);
                isSeeding = true;
            }

            context.SaveChanges(); // make sure IDs exist for FK relations

            // --- MovieGenres ---
            if (!context.MovieGenres.Any())
            {
                var movieGenres = new List<MovieGenre>();
                var genreIds = context.Genres.Select(g => g.GenreId).ToList();
                var movieIds = context.Movies.Select(m => m.MovieId).ToList();

                foreach (var movieId in movieIds)
                {
                    var selectedGenres = genreIds.OrderBy(_ => random.Next()).Take(random.Next(1, 4));
                    foreach (var genreId in selectedGenres)
                    {
                        movieGenres.Add(new MovieGenre
                        {
                            MovieId = movieId,
                            GenreId = genreId
                        });
                    }
                }
                context.MovieGenres.AddRange(movieGenres);
                isSeeding = true;
            }

            // --- MovieCasts ---
            if (!context.MovieCasts.Any())
            {
                var movieCasts = new List<MovieCast>();
                var actorIds = context.Actors.Select(a => a.ActorId).ToList();
                var movieIds = context.Movies.Select(m => m.MovieId).ToList();

                foreach (var movieId in movieIds)
                {
                    var selectedActors = actorIds.OrderBy(_ => random.Next()).Take(random.Next(2, 5));
                    foreach (var actorId in selectedActors)
                    {
                        movieCasts.Add(new MovieCast
                        {
                            MovieId = movieId,
                            ActorId = actorId,
                            Role = $"Role {random.Next(1, 100)}"
                        });
                    }
                }
                context.MovieCasts.AddRange(movieCasts);
                isSeeding = true;
            }

            // --- MovieDirections ---
            if (!context.MovieDirections.Any())
            {
                var movieDirections = new List<MovieDirection>();
                var directorIds = context.Directors.Select(d => d.DirectorId).ToList();
                var movieIds = context.Movies.Select(m => m.MovieId).ToList();

                foreach (var movieId in movieIds)
                {
                    var directorId = directorIds[random.Next(directorIds.Count)];
                    movieDirections.Add(new MovieDirection
                    {
                        MovieId = movieId,
                        DirectorId = directorId
                    });
                }
                context.MovieDirections.AddRange(movieDirections);
                isSeeding = true;
            }

            // --- Ratings ---
            if (!context.Ratings.Any())
            {
                var ratings = new List<Rating>();
                var movieIds = context.Movies.Select(m => m.MovieId).ToList();
                var reviewerIds = context.Reviewers.Select(r => r.ReviewerId).ToList();

                foreach (var movieId in movieIds)
                {
                    foreach (var reviewerId in reviewerIds.OrderBy(_ => random.Next()).Take(random.Next(2, 4)))
                    {
                        ratings.Add(new Rating
                        {
                            MovieId = movieId,
                            ReviewerId = reviewerId,
                            ReviewStars = random.Next(1, 6), // 1–5 stars
                            NumberOfRating = random.Next(1, 11)
                        });
                    }
                }
                context.Ratings.AddRange(ratings);
                isSeeding = true;
            }

            return isSeeding;
        }

    }
}
