using Lpsgrupo.WebApi.Presentation.Exceptions;
using Lpsgrupo.WebApi.Presentation.Interfaces.Services;
using Lpsgrupo.WebApi.Presentation.Responses;

namespace Lpsgrupo.WebApi.Presentation.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiKeyMiddleware(RequestDelegate next, IApiKeyService apiKeyService)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IApiKeyService apiKeyService)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var traceId = context.TraceIdentifier;
            context.Response.StatusCode = 401;

            var contentResponse = new ApiErrorResponse(
                type: typeof(ApiKeyException).Name,
                //errorCode: (int)ErrorCode.ApiKeyNotAuthorized,
                traceId: traceId
            );

            if (!context.Request.Headers.TryGetValue("x-api-key", out var extractedApiKey))
            {
                contentResponse.Message = "Api Key was not provided.";
                await context.Response.WriteAsJsonAsync(contentResponse);

                return;
            }

            if (!Guid.TryParse(extractedApiKey, out Guid validGuid))
            {
                contentResponse.Message = "Invalid Api Key format.";
                await context.Response.WriteAsJsonAsync(contentResponse);

                return;
            }

            var result = apiKeyService.IsApiKeyValid(extractedApiKey!);

            if (!result)
            {
                contentResponse.Message = "Unauthorized client.";
                await context.Response.WriteAsJsonAsync(contentResponse);

                return;
            }

            await _next(context);
        }
    }
}
