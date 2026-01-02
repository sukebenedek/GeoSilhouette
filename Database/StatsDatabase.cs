using SQLite;

namespace GeoSilhouette.Database;

public class StatsDatabase
{
    private SQLiteAsyncConnection? database;

    private async Task Init()
    {
        if (database != null)
            return;

        database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.FLags);

        await database.CreateTableAsync<StatEntity>();

        // Ensure row exists
        if (await database.Table<StatEntity>().CountAsync() == 0)
        {
            await database.InsertAsync(new StatEntity());
        }
    }

    public async Task<StatEntity> GetStats()
    {
        await Init();
        return await database!.Table<StatEntity>().FirstAsync();
    }

    public async Task SaveStats(StatEntity stats)
    {
        await Init();
        await database!.UpdateAsync(stats);
    }

    public async Task ResetStats()
    {
        await Init();
        await database!.DeleteAllAsync<StatEntity>();
        await database!.InsertAsync(new StatEntity());
    }
}
