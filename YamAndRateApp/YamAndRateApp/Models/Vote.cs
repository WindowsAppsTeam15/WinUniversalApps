namespace YamAndRateApp.Models
{
    using Parse;

    [ParseClassName("Vote")]
    public class Vote : ParseObject
    {
        [ParseFieldName("value")]
        public int Value
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        /*
        public string User
        {
            get { return GetProperty<CategoryType>(); }
            set { SetProperty<CategoryType>(value); }
        }
        */
    }
}