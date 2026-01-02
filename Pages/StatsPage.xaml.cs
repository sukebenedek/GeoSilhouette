using GeoSilhouette.ViewModels;
namespace GeoSilhouette.Pages;

public partial class StatsPage : ContentPage
{
    //public ViewModels.StatViewModel statsVm = new ViewModels.StatViewModel
    //    {
    //        RoundsPlayed = 42,
    //        TotalGuesses = 180,
    //        CorrectGuesses = 121,
    //        TotalPlaytime = TimeSpan.FromMinutes(95),
    //        AvgTimePerRound = TimeSpan.FromSeconds(135),
    //        EasyGames = 20,
    //        MediumGames = 15,
    //        HardGames = 7
    //    };
    public StatsPage(ViewModels.StatViewModel vm)/**/
	{
        InitializeComponent();
        BindingContext = vm;

        //BindingContext = statsVm;

    }
}