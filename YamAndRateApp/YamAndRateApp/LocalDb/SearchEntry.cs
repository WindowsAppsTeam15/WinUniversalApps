namespace YamAndRateApp.LocalDb
{
    using SQLite.Net.Attributes;

    [Table("SearchEntry")]
    public class SearchEntry
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Pattern { get; set; }

        public override string ToString()
        {
            return this.Pattern;
        }
    }
}
