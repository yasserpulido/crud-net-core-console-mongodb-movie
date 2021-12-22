using Core.Interfaces;

namespace Infrastructure.Models
{
    public class MovieDatabaseSettings : IMovieDatabaseSettings
    {
        public string MoviesCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
