using CommunityToolkit.Maui;
using GeoSilhouette.Database;
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
            builder.Services.AddSingleton<ViewModels.ChoseViewModel>();
            builder.Services.AddSingleton<ViewModels.GameViewModel>();
            builder.Services.AddSingleton<ViewModels.StatViewModel>();

            builder.Services.AddSingleton<Pages.ChosePage>();
            builder.Services.AddSingleton<Pages.GamePage>();
            builder.Services.AddSingleton<Pages.StatsPage>();

            builder.Services.AddSingleton<StatsDatabase>();



#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
