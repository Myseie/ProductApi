using MongoDB.Driver;
using ProductApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using ProductApi.Data;


namespace ProductApi.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ProductApi");
            _products = database.GetCollection<Product>("Products");
        }

        public List<Product> GetAllProducts() =>
            _products.Find(product => true).ToList();

        public Product GetProductById(string id) =>
           _products.Find(product => product.Id == id).FirstOrDefault();
        public void CreateProduct(Product newProduct) =>
            _products.InsertOne(newProduct);

        public void UpdateProduct(string id, Product updatedProduct)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            _products.ReplaceOne(filter, updatedProduct);
        }


        public void DeleteProduct(string id) =>
         _products.DeleteOne(product => product.Id == id);
    }
}
