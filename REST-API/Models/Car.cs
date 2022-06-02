using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public String carBrand { get; set; }
        public String carModel { get; set; }

        public String weekday { get; set; }
        public String time { get; set; }

        public Boolean repeat { get; set; }


    }
}
