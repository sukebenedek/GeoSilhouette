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
            await Shell.Current.GoToAsync($"{nameof(Pages.GamePage)}?difficulty={difficultyLevel}");
        }

        [RelayCommand]
        private async Task ShowInfo(string difficultyLevel)
        {
            string title = "Difficulty Info";
            string message = "";

            // Determine message based on the difficulty logic you provided
            switch (difficultyLevel.ToLower())
            {
                case "easy":
                    title = "Newborn Infant";
                    message = "Filters: Population > 7,000,000 and excludes Africa OR Population > 350,000 in Europe. With distances showing.";
                    break;

                case "medium":
                    title = "Medium";
                    message = "Filters: Population > 500,000. Without distances showing.";
                    break;

                case "hard":
                    title = "Violent Agonizing Death";
                    message = "Filters: None. All UN recognized countries included where Population < 25,000,000. Without distances showing.";
                    break;

                default:
                    message = "Unknown difficulty.";
                    break;
            }

            await Shell.Current.DisplayAlert(title, message, "OK");
        }
    }
}
