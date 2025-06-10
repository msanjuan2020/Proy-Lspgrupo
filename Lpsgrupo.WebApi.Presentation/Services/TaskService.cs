using Lpsgrupo.WebApi.Presentation.Data;
using Lpsgrupo.WebApi.Presentation.Interfaces;
using Lpsgrupo.WebApi.Presentation.Models;
using MongoDB.Driver;

namespace Lpsgrupo.WebApi.Presentation.Services
{
   
    public class TaskService : ITaskService
    {

        //private readonly IMongoCollection<TaskItem> _tasks;
        //public TaskService(IConfiguration configuration)
        //{
        //    var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        //    var database = client.GetDatabase(configuration["Database"]);
        //    _tasks = database.GetCollection<TaskItem>(configuration["Collection"]);
        //}

        private readonly MongoDbContext _mongoDbContext;

        public TaskService(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task Create(TaskItem task)
        {
            await _mongoDbContext.TaskItems.InsertOneAsync(task);
        }

        public async Task Delete(string id)
        {
            await _mongoDbContext.TaskItems.DeleteOneAsync(task => task.Id == id);
        }

        public async Task<List<TaskItem>> GetAll()
        {
            return await _mongoDbContext.TaskItems.Find(_ => true).ToListAsync();
        }

        public async Task<TaskItem> GetById(string id)
        {
            return await _mongoDbContext.TaskItems.Find(task => task.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, TaskItem task)
        {
            await _mongoDbContext.TaskItems.ReplaceOneAsync(t => t.Id == id, task);
        }
    }
}
