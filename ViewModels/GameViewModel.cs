using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [ObservableProperty]
        private string filterText;

        [ObservableProperty]
        private ObservableCollection<Country> selectableCountries = [];

        [ObservableProperty]
        private Country selectedCountry;

        public Action<string, string, int, bool> UI_AddGuessToScreen;
        public Action UI_ClearGuesses;
        public Action UI_AddPlaceholderToUI;

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

            FilterText = "";
            SelectedCountry = null;
            SelectableCountries = new ObservableCollection<Country>(FilteredCountries).OrderBy(s => s.realName).ToObservableCollection();

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



        partial void OnFilterTextChanged(string value)
        {
            UpdateFilteredCountries(value);

            SelectedCountry = null;
            SelectableCountries = SelectableCountries.OrderBy(s => s.realName).ToObservableCollection();

            if(SelectableCountries.Count == 1)
            {
                SelectedCountry = SelectableCountries[0];
            }
        }

        private void UpdateFilteredCountries(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // Reset to full list if empty
                SelectableCountries = new ObservableCollection<Country>(FilteredCountries).ToObservableCollection();
            }
            else
            {
                // Filter the list (case-insensitive)
                var filtered = FilteredCountries
                    .Where(c => c.realName.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                SelectableCountries = new ObservableCollection<Country>(filtered);
            }
        }

        [RelayCommand]
        private async Task Guess()
        {
            if(SelectedCountry == null)
                await Shell.Current.DisplayAlert("Error", "Please select a country!", "OK");
            else
            {
                //await Shell.Current.DisplayAlert("Error", $"{SelectedCountry.cca2}, {ChosenOne.cca2}", "OK");
                if (SelectedCountry.cca2 == ChosenOne.cca2)
                {
                    OnPageAppearing();
                    UI_ClearGuesses?.Invoke();
                    UI_AddPlaceholderToUI?.Invoke();

                }
                else
                {
                    UI_AddGuessToScreen?.Invoke(SelectedCountry.realName, Country.GetDirection(SelectedCountry, ChosenOne), Country.GetDistance(SelectedCountry, ChosenOne), Difficulty == "easy");
                }







            }
        }
    }
}
