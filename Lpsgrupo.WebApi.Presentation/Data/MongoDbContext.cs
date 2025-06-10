
using Lpsgrupo.WebApi.Presentation.Models;
using MongoDB.Driver;

namespace Lpsgrupo.WebApi.Presentation.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase(configuration["Database"]);
        }

        public IMongoCollection<TaskItem> TaskItems => _database.GetCollection<TaskItem>("Tasks");
        // agregar mas collections según sea necesario
        // ejemplos  public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    }
}
