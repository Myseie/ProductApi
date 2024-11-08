    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;
    using System.ComponentModel.DataAnnotations;

    namespace ProductApi.Model
    {
        public class Product
        {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
            public string Name { get; set; }

            public string Description { get; set; }

            [Required]
            public decimal Price { get; set; }

            public int Stock { get; set; }

            public string Category { get; set; }
        }
    }
