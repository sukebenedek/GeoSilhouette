using CommunityToolkit.Mvvm.ComponentModel;
using GeoSilhouette.Database;

namespace GeoSilhouette.ViewModels;

public partial class StatViewModel : ObservableObject
{
    private readonly StatsDatabase _db;

    public StatViewModel(StatsDatabase db)
    {
        _db = db;
        _ = LoadAsync();
    }

    [ObservableProperty] private int roundsPlayed;
    [ObservableProperty] private int totalGuesses;
    [ObservableProperty] private int correctGuesses;
    [ObservableProperty] private TimeSpan totalPlaytime;
    [ObservableProperty] private TimeSpan avgTimePerRound;
    [ObservableProperty] private int easyGames;
    [ObservableProperty] private int mediumGames;
    [ObservableProperty] private int hardGames;

    public double Accuracy =>
        TotalGuesses == 0 ? 0 : (double)CorrectGuesses / TotalGuesses * 100;

    partial void OnTotalGuessesChanged(int _) => OnPropertyChanged(nameof(Accuracy));
    partial void OnCorrectGuessesChanged(int _) => OnPropertyChanged(nameof(Accuracy));

    private async Task LoadAsync()
    {
        var s = await _db.GetStats();

        RoundsPlayed = s.RoundsPlayed;
        TotalGuesses = s.TotalGuesses;
        CorrectGuesses = s.CorrectGuesses;

        TotalPlaytime = TimeSpan.FromSeconds(s.TotalPlaytimeSeconds);
        AvgTimePerRound = TimeSpan.FromSeconds(s.AvgTimePerRoundSeconds);

        EasyGames = s.EasyGames;
        MediumGames = s.MediumGames;
        HardGames = s.HardGames;
    }

    public async Task SaveAsync()
    {
        await _db.SaveStats(new StatEntity
        {
            Id = 1,
            RoundsPlayed = RoundsPlayed,
            TotalGuesses = TotalGuesses,
            CorrectGuesses = CorrectGuesses,
            TotalPlaytimeSeconds = TotalPlaytime.TotalSeconds,
            AvgTimePerRoundSeconds = AvgTimePerRound.TotalSeconds,
            EasyGames = EasyGames,
            MediumGames = MediumGames,
            HardGames = HardGames
        });
    }
}
