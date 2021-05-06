using System;
using System.Collections.Generic;
using dotnet_search_mongo.Data;
using dotnet_search_mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace dotnet_search_mongo.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(DbClient client)
        {
            _products = client.All();
        }

        public List<Product> All() => _products.Find(product => true).ToList();

        public Product AddProduct(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public Object Query(string s, string sort, int? queryPage)
        {
            var filter = Builders<Product>.Filter.Empty;

            if (!string.IsNullOrEmpty(s))
            {
                filter = Builders<Product>.Filter.Regex("Title", new BsonRegularExpression(s, "i")) |
                         Builders<Product>.Filter.Regex("Description", new BsonRegularExpression(s, "i"));
            }

            var find = _products.Find(filter);

            if (sort == "asc")
            {
                find = find.SortBy(p => p.Price);
            }
            else if (sort == "desc")
            {
                find = find.SortByDescending(p => p.Price);
            }

            int page = queryPage.GetValueOrDefault(1) == 0 ? 1 : queryPage.GetValueOrDefault(1);
            int perPage = 9;
            var total = find.CountDocuments();

            return new
            {
                data = find.Skip((page - 1) * perPage).Limit(perPage).ToList(),
                total,
                page,
                last_page = total / perPage
            };
        }
    }
}