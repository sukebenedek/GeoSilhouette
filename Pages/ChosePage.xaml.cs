namespace GeoSilhouette.Pages;

public partial class ChosePage : ContentPage
{
	public ChosePage(ViewModels.ChoseViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}