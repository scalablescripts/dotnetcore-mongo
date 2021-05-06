using dotnet_search_mongo.Models;
using MongoDB.Driver;

namespace dotnet_search_mongo.Data
{
    public class DbClient
    {
        private readonly IMongoCollection<Product> _products;

        public DbClient()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("dotnet_search");
            _products = database.GetCollection<Product>("products");
        }

        public IMongoCollection<Product> All() => _products;
    }
}