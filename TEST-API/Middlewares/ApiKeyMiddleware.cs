using TEST_API.Databases;

namespace TEST_API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyName = "ApiKey";
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided");

                return;
            }

            var apiKey = new FakeDatabase().GetDb().Where(x => x.ApiKey == extractedApiKey).FirstOrDefault();

            if (apiKey == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");

                return;
            }

            if (extractedApiKey != apiKey.ApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");

                return;
            }
            await _next(context);
        }
    }
}
