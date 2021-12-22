using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.Entities
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int Released { get; set; }
        public int Runtime { get; set; }
        public string Genre { get; set; } = null!;
        public string Plot { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Director { get; set; } = null!;
    }
}
