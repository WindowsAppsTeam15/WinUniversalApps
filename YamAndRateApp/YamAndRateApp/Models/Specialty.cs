namespace YamAndRateApp.Models
{
    using Parse;

    [ParseClassName("specialty")]
    public class Specialty : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}
