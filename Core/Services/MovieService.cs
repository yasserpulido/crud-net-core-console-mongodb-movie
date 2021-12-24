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
                return await _movie.CreateMovie(movie);
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
                return await _movie.GetMovieBy(title, released);
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
                return await _movie.GetAllMovies();
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
                return await _movie.UpdateMovie(movie);
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
                return await _movie.DeleteMovie(movie);
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
                var movies = await _movie.GetAllMovies();

                if (result != null)
                {
                    string json = JsonSerializer.Serialize(movies);
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
