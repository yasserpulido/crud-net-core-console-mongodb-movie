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

        public async Task CreateMovie(Movie movie)
        {
            await _movie.CreateMovie(movie);
        }
    }
}
