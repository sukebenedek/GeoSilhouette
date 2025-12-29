using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.ViewModels
{
    public partial class ChoseViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task Chose(string difficultyLevel)
        {
            // Use the variable 'difficultyLevel' inside the curly braces
            await Shell.Current.GoToAsync($"{nameof(Pages.GamePage)}?difficulty={difficultyLevel}");
        }
    }
}
