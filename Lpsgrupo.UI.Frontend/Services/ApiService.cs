using Lpsgrupo.UI.Frontend.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Lpsgrupo.UI.Frontend.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGrupoLps");
        }

        public async Task<T> GetAsync<T>(string endpoint, Dictionary<string, string>? parameters = null, string? token = null)
        {
            string queryString = string.Empty;

            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (parameters != null)
            {
                var encodedContent = new FormUrlEncodedContent(parameters);
                queryString = $"?{await encodedContent.ReadAsStringAsync()}";
            }

            var response = await _httpClient.GetAsync($"{endpoint}{queryString}");

            if (response.Content == null || response.Content.Headers.ContentLength == 0)
            {
                throw new HttpRequestException("La respuesta viene vacía");
            }

            try
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error al deserializar la respuesta JSON", ex);
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data, string? token = null)
        {
            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.Content == null || response.Content.Headers.ContentLength == 0)
            {
                throw new HttpRequestException("La respuesta viene vacía");
            }

            try
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error al deserializar la respuesta JSON", ex);
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data, string? token = null)
        {
            if (token != null)
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);

            if (response.Content == null || response.Content.Headers.ContentLength == 0)
            {
                throw new HttpRequestException("La respuesta viene vacía");
            }

            try
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error al deserializar la respuesta JSON", ex);
            }
        }
    }
}
