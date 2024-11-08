using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductApi.Data;
using ProductApi.Service;
namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;

                if (string.IsNullOrEmpty(settings.ConnectionString))
                {
                    throw new ArgumentNullException(nameof(settings.ConnectionString), "MongoDB ConnectionString �r null eller tom.");
                }

                return new MongoClient(settings.ConnectionString);
            });


            builder.Services.AddScoped<ProductService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
