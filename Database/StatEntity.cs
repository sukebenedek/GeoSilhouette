using SQLite;

namespace GeoSilhouette.Database;

[Table("Stats")]
public class StatEntity
{
    [PrimaryKey]
    public int Id { get; set; } = 1; // single-row table

    public int RoundsPlayed { get; set; }
    public int TotalGuesses { get; set; }
    public int CorrectGuesses { get; set; }

    public double TotalPlaytimeSeconds { get; set; }
    public double AvgTimePerRoundSeconds { get; set; }

    public int EasyGames { get; set; }
    public int MediumGames { get; set; }
    public int HardGames { get; set; }
}
