using Core.Entities;

namespace Core.Interfaces
{
    public interface IMovie
    {
        Task<bool> CreateMovie(Movie movie);
        Task<List<Movie>?> GetAllMovies();
        Task<Movie?> GetMovieBy(string title, int released);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(Movie movie);
    }
}
