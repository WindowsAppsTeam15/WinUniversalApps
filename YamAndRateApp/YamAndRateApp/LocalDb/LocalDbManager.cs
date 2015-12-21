namespace YamAndRateApp.LocalDb
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;    

    using Windows.Storage;

    public class LocalDbManager
    {
        private SQLiteAsyncConnection GetDbConnectionAsync()
        {
            var dbFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "yamAndRate.sqlite");

            var connectionFactory =
                new Func<SQLiteConnectionWithLock>(
                    () =>
                    new SQLiteConnectionWithLock(
                        new SQLitePlatformWinRT(),
                        new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }

        public async void InitAsync()
        {
            var connection = this.GetDbConnectionAsync();
            await connection.CreateTableAsync<SearchEntry>();
        }

        public async Task<int> InsertSearchEntryAsync(SearchEntry entry)
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.InsertAsync(entry);
            return result;
        }

        public async Task<List<SearchEntry>> GetAllSearchEntriesAsync()
        {
            var connection = this.GetDbConnectionAsync();
            var result = await connection.Table<SearchEntry>().ToListAsync();
            return result;
        }
    }
}
