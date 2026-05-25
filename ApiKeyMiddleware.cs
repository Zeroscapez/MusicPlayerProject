namespace MusicPlayer
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER = "X-API-Key";
        private const string GUEST_TOKEN_HEADER = "X-Guest-Token";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Allow Swagger UI and Remote API docs without API key
            var path = context.Request.Path.Value ?? string.Empty;
            if (path.StartsWith("/swagger") || path == "/" || path.StartsWith("/index") || path.StartsWith("/api/room/join"))
            {
                await _next(context);
                return;
            }

            //Check for host API key in header, compare to config value
            if (context.Request.Headers.TryGetValue(API_KEY_HEADER, out var apikey))
            {
                var config = context.RequestServices.GetRequiredService<IConfiguration>();
                if (apikey == config["ApiKey"])
                {
                    context.Items["IsHost"] = true; // mark as host for controllers to check
                    await _next(context);
                    return;
                }
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid API key" });
                return;
            }

            //Check Guest Tokens
            if (context.Request.Headers.TryGetValue(GUEST_TOKEN_HEADER, out var guestToken))
            {
                var room = context.RequestServices.GetRequiredService<RoomService>();
                if (room.ValidateGuestToken(guestToken))
                {
                    context.Items["IsHost"] = false; // mark as guest for controllers to check
                    await _next(context);
                    return;
                }

                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid guest token" });
                return;
            }

            // No credentials at all
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new { error = "Authentication required" });
        }


    }
}
