using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GeoSilhouette.ViewModels;

[QueryProperty(nameof(Difficulty), "difficulty")]
public partial class GameViewModel : ObservableObject
{
    private readonly StatViewModel _stats;

    public GameViewModel(StatViewModel stats)
    {
        _stats = stats;
    }

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

    private DateTime _roundStart;

    // --------------------
    // PAGE APPEARING
    // --------------------
    [RelayCommand]
    public async Task OnPageAppearing()
    {
        _roundStart = DateTime.UtcNow;
        _stats.RoundsPlayed++;
        await _stats.SaveAsync();
        switch (Difficulty)
        {
            case "easy":
                FilteredCountries = Countries
                    .Where(c => c.population > 7_000_000 && !c.continents.Contains("Africa"))
                    .ToObservableCollection();
                break;

            case "medium":
                FilteredCountries = Countries
                    .Where(c => c.population > 500_000)
                    .ToObservableCollection();
                break;

            case "hard":
                FilteredCountries = Countries.ToObservableCollection();
                break;
        }

        FilterText = "";
        SelectedCountry = null;

        SelectableCountries = FilteredCountries
            .OrderBy(c => c.realName)
            .ToObservableCollection();

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
        await _stats.SaveAsync();
    }

    // --------------------
    // FILTERING
    // --------------------
    partial void OnFilterTextChanged(string value)
    {
        UpdateFilteredCountries(value);

        SelectedCountry = null;
        SelectableCountries = SelectableCountries
            .OrderBy(c => c.realName)
            .ToObservableCollection();

        if (SelectableCountries.Count == 1)
            SelectedCountry = SelectableCountries[0];
    }

    private void UpdateFilteredCountries(string query)
    {
        SelectableCountries = string.IsNullOrWhiteSpace(query)
            ? FilteredCountries.ToObservableCollection()
            : FilteredCountries
                .Where(c => c.realName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToObservableCollection();
    }

    // --------------------
    // GUESS LOGIC + STATS
    // --------------------
    [RelayCommand]
    private async Task Guess()
    {

        if (SelectedCountry == null)
        {
            await Shell.Current.DisplayAlert("Error", "Please select a country!", "OK");
            return;
        }

        _stats.TotalGuesses++;
        await _stats.SaveAsync();

        if (SelectedCountry.cca2 == ChosenOne.cca2)
        {
            _stats.CorrectGuesses++;

            var roundTime = DateTime.UtcNow - _roundStart;
            _stats.TotalPlaytime += roundTime;
            _stats.AvgTimePerRound =
                TimeSpan.FromSeconds(
                    _stats.TotalPlaytime.TotalSeconds / _stats.RoundsPlayed
                );

            switch (Difficulty)
            {
                case "easy": _stats.EasyGames++; break;
                case "medium": _stats.MediumGames++; break;
                case "hard": _stats.HardGames++; break;
            }

            UI_ClearGuesses?.Invoke();
            UI_AddPlaceholderToUI?.Invoke();
            await _stats.SaveAsync();
            await OnPageAppearing();
        }
        else
        {
            UI_AddGuessToScreen?.Invoke(
                SelectedCountry.realName,
                Country.GetDirection(SelectedCountry, ChosenOne),
                Country.GetDistance(SelectedCountry, ChosenOne),
                Difficulty == "easy"
            );
            await _stats.SaveAsync();

        }
        await _stats.SaveAsync();
    }
}
