using Core.Entities;

namespace Core.Interfaces
{
    public interface IMovie
    {
        Task CreateMovie(Movie movie);
        //List<Movie> GetAllMovies();
        //Movie GetMovieBy(string title, int released);
        //bool UpdateMovie(Movie movie);
        //bool DeleteMovie(string title, int released);
    }
}
