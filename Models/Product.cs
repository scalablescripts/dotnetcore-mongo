using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnet_search_mongo.Models
{
    public class Product
    {
        [BsonId] public ObjectId _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

        public Product()
        {
            _id = ObjectId.GenerateNewId();
        }
    }
}