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

        public async Task CreateMovie(Movie movie)
        {
            await _collection.InsertOneAsync(movie);
        }
    }
}
