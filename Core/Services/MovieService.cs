using System.Text.Json;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class MovieService
    {
        private readonly IMovie _movie;

        public MovieService(IMovie movie)
        {
            _movie = movie;
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
                bool result = await _movie.CreateMovie(movie);
                return result;
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
        public async Task<Movie?> GetMovie(string title, int released)
        {
            try
            {
                var result = await _movie.GetMovieBy(title, released);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
                var result = await _movie.GetAllMovies();
                return result;
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
                bool result = await _movie.UpdateMovie(movie);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
                var result = await _movie.DeleteMovie(movie);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Generate a json file of movies.
        /// </summary>
        /// <returns>A value to know if the file was generate or not.</returns>
        public async Task<bool> GenerateJson()
        {
            try
            {
                var result = await _movie.GetAllMovies();

                if (result != null)
                {
                    string json = JsonSerializer.Serialize(result);
                    await File.WriteAllTextAsync(@"C:\Users\pulid\Documents\movie.json", json);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
