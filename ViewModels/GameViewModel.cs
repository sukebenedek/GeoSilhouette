using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GeoSilhouette.ViewModels
{
    [QueryProperty(nameof(Difficulty), "difficulty")]
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Country> countries = App.Countries.ToObservableCollection();


        [ObservableProperty]
        private string difficulty;

        [ObservableProperty]
        private ObservableCollection<Country> filteredCountries = [];


        [ObservableProperty]
        private Country chosenOne;

        public async void OnPageAppearing()
        {
            //Shell.Current.DisplayAlert("Loaded", Difficulty, "OK");
            switch (Difficulty)
            {
                case ("easy"):
                    FilteredCountries = Countries.Where(c => c.population > 7000000 && !c.continents.Contains("Africa")).ToObservableCollection();
                    break;
                case ("medium"):
                    FilteredCountries = Countries.Where(c => c.population > 500000).ToObservableCollection();
                    break;
                case ("hard"):
                    FilteredCountries = Countries.ToObservableCollection();
                    break;
            }

            var rnd = new Random();

            while (true)
            {
                var candidate = FilteredCountries[rnd.Next(FilteredCountries.Count)];

                try
                {
                    if (await candidate.ImageExistsAsync())
                    {
                        ChosenOne = candidate;
                        return;
                    }
                }
                catch { }
            }
        }
    }
}
