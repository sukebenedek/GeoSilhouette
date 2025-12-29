using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace GeoSilhouette
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Comic Sans MS.ttf", "ComicSans");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ViewModels.MainViewModel>();
            builder.Services.AddSingleton<Pages.ChosePage>();
            builder.Services.AddSingleton<ViewModels.ChoseViewModel>();
            builder.Services.AddTransient<Pages.GamePage>();
            builder.Services.AddTransient<ViewModels.GameViewModel>();
            builder.Services.AddSingleton<Pages.StatsPage>();
            builder.Services.AddSingleton<ViewModels.StatViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
