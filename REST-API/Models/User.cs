using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        // Zufällige ID wird erstellt
        public string? Id { get; set; }
        public string username { get; set; }
        public string passwort { get; set; }
        
    }
}
