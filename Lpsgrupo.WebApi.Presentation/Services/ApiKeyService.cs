using Lpsgrupo.WebApi.Presentation.Configuration;
using Lpsgrupo.WebApi.Presentation.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Lpsgrupo.WebApi.Presentation.Services
{
    //public class ApiKeyService(IOptions<ApiKeySettings> apiKeySettings) : IApiKeyService
    //{
    //    public bool IsApiKeyValid(string apiKey)
    //    {
    //        return apiKey.Equals(apiKeySettings.Value.ApiKey);
    //    }
    //}
    public class ApiKeyService : IApiKeyService
    {
        private readonly string _validApiKey;

        public ApiKeyService(IConfiguration configuration)
        {
            _validApiKey = configuration["ApiKeySettings:ApiKey"];  // Obtiene la clave API desde appsettings
        }

        public bool IsApiKeyValid(string apiKey)
        {
            return _validApiKey == apiKey;  // Compara la clave proporcionada con la clave válida
        }
    }

}
