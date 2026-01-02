using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSilhouette.Database
{
    public static class DatabaseConstants
    {
        public const string DatabaseFileName = "Statistics.db3";

        public const SQLite.SQLiteOpenFlags FLags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

    }
}
