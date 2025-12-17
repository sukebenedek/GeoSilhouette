namespace GeoSilhouette.Pages;

public partial class GamePage : ContentPage
{
	public GamePage(ViewModels.GameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    
}