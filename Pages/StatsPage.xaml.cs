namespace GeoSilhouette.Pages;

public partial class StatsPage : ContentPage
{
	public StatsPage(ViewModels.StatViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}