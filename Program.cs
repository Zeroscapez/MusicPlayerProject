namespace MusicPlayer
{
    internal static class Program
    {
        public static PlayerService PlayerService { get; } = new PlayerService();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Start the web API in a background thread
            Thread apiThread = new Thread(() =>
            {
                var builder = WebApplication.CreateBuilder();

                // Tell Kestrel what port to listen on
                //builder.WebHost.UseUrls("http://localhost:5000");
                builder.WebHost.UseUrls("http://0.0.0.0:5000");

                // Register our PlayerService as a singleton so controllers can get it
                builder.Services.AddSingleton(PlayerService);
                builder.Services.AddControllers();

                //Swagger
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                //CORS - Allow any origin (for development purposes)
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                });
                var app = builder.Build();

                //Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
                //

                //Fille control in browser
                app.UseStaticFiles();

                //Cors
                app.UseCors();


                //Logging middleware
                app.UseMiddleware<LoggingMiddleware>();

                //Auth Middleware
                app.UseMiddleware<ApiKeyMiddleware>();


                app.MapControllers();
                app.Run();
            });

            apiThread.IsBackground = true; // Dies when the WinForms app closes
            apiThread.Start();

            Application.Run(new Form1());
        }
    }
}