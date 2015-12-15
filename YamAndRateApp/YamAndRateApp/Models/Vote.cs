namespace YamAndRateApp.Models
{
    public class Vote
    {
        public Vote(int value, string user)
        {
            this.Value = value;
            this.User = user;
        }

        public int Value { get; set; }

        public string User { get; set; }
    }
}