using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplicationTrackerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            //MAUI setup
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Path to SQLite database
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "JobApplicationDatabase.db");

            // Register DatabaseService with the dbPath
            builder.Services.AddSingleton<DatabaseService>(s => new DatabaseService(dbPath));

            // Register MainPage so it can use DI
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Register pages for Shell
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddApplication>();
            builder.Services.AddTransient<EditApplication>();

            return builder.Build();
        }
    }
}
