using CommunityToolkit.Maui;
using ExpenseTracker.Data;
using ExpenseTracker.Pages;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace ExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("RaphLanokFuture-PvDx.ttf", "RaphLanokFuture");
                    fonts.AddFont("Montserrat-Italic-VariableFont_wght", "Montserrat1");
                    fonts.AddFont("Montserrat-VariableFont_wght", "Montserrat2");
                });
            builder.Services.AddSingleton<DatabaseService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}