using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeoSilhouette.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Play()
        {
            // navigate to ChoosePage (assuming registered in AppShell)
            await Shell.Current.GoToAsync(nameof(Pages.ChosePage));
        }
    }
}
