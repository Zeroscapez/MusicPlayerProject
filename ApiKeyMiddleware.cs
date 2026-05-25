namespace MusicPlayer
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-API-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Allow Swagger UI and Remote API docs without API key
            var path = context.Request.Path.Value ?? string.Empty;
            if (path.StartsWith("/swagger") || path == "/" || path.StartsWith("/index"))
            {
                await _next(context);
                return;
            }

            //Check for API key in header
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER, out var key))
            {
                context.Response.StatusCode = 0;
                await context.Response.WriteAsJsonAsync(new { error = "API key missing" });
                return;
            }

            //Check if key is correct
            var config = context.RequestServices.GetRequiredService<IConfiguration>();
            var validKey = config["ApiKey"];

            if (key != validKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid API key" });
                return;
            }

            // Key is valid, continue to the controller
            await _next(context);
        }


    }
}
