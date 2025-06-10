namespace Lpsgrupo.UI.Frontend.Services.Interfaces
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? parameters = null, string? token = null);

        Task<T> PostAsync<T>(string endpoint, object data, string? token = null);

        Task<T> PutAsync<T>(string endpoint, object data, string? token = null);
    }
}
