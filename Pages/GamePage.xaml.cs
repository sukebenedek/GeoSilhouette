using GeoSilhouette.ViewModels;

namespace GeoSilhouette.Pages;

public partial class GamePage : ContentPage
{
	public GamePage(ViewModels.GameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Check if the BindingContext is actually your ViewModel
        if (BindingContext is GameViewModel vm)
        {
            vm.OnPageAppearing();
        }
    }
}