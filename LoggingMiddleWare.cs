namespace MusicPlayer
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logPath;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;

            //Create a log folder in the same directory as the executable
            var logDir = Path.Combine(AppContext.BaseDirectory, "logs");
            Directory.CreateDirectory(logDir);
            _logPath = Path.Combine(logDir, "requests.log");

        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value ?? string.Empty;

            // Don't log the polling endpoint, it's too noisy
            if (path == "/api/player/nowplaying")
            {
                await _next(context);
                return;
            }

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var method = context.Request.Method;


            //Let the request continue through 
            await _next(context);

            //After the response is generated, log the request and response status code
            var status = context.Response.StatusCode;

            var line = $"[{timestamp}] {method} {path} => {status}";
            Console.WriteLine(line);
            await File.AppendAllTextAsync(_logPath, line + Environment.NewLine);
        }
    }

}