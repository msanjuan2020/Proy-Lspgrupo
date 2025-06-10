using Lspgrupo.Cross.Entities.Task;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Lpsgrupo.UI.Frontend.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskItemDto>> GetTasksAsync(string url)
        {
            var response = await _httpClient.GetAsync($"api/task{url}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TaskItemDto>>();
        }
        public async Task<TaskItemDto> GetTaskByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/tasks/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TaskItemDto>();
        }
        public async Task CreateTaskAsync(TaskItemDto task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tasks", task);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateTaskAsync(TaskItemDto task)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteTaskAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
