using Core.Entities;
using Core.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovie
    {
        private readonly IMongoCollection<Movie> _collection;

        public MovieRepository(IOptions<MovieDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Movie>(settings.Value.MoviesCollectionName);
        }

        /// <summary>
        /// Create a movie file.
        /// </summary>
        /// <param name="movie">Movie data.</param>
        /// <returns>A value to know if the data was created or not.</returns>
        public async Task<bool> CreateMovie(Movie movie)
        {
            try
            {
                await _collection.InsertOneAsync(movie);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get all movies files.
        /// </summary>
        /// <returns>A list of movies.</returns>
        public async Task<List<Movie>?> GetAllMovies()
        {
            try
            {
                var movies = await _collection.FindAsync(m => true).Result.ToListAsync();
                return movies;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Get a specific movie file.
        /// </summary>
        /// <param name="title">Title of movie.</param>
        /// <param name="released">Released of movie.</param>
        /// <returns>A movie file.</returns>
        public async Task<Movie?> GetMovieBy(string title, int released)
        {
            try
            {
                var movies =  await _collection.FindAsync(m => m.Title.ToUpper() == title && m.Released == released).Result.FirstOrDefaultAsync();
                return movies;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Update a movie file data.
        /// </summary>
        /// <param name="movie">Movie data.</param>
        /// <returns>A value to know if the data was updated or not.</returns>
        public async Task<bool> UpdateMovie(Movie movie)
        {
            try
            {
                await _collection.ReplaceOneAsync(m => m.Id == movie.Id, movie);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a movie file.
        /// </summary>
        /// <param name="movie">Movie data.</param>
        /// <returns>A value to know if the data was deleted or not.</returns>
        public async Task<bool> DeleteMovie(Movie movie)
        {
            try
            {
                await _collection.DeleteOneAsync(m => m.Id == movie.Id);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }
    }
}
