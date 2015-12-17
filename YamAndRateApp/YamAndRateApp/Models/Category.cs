namespace YamAndRateApp.Models
{
    using Parse;

    [ParseClassName("Category")]
    public class Category : ParseObject
    {
        public CategoryType CategoryType
        {
            get { return (CategoryType)this.CategoryTypeForParse; }
            set { this.CategoryTypeForParse = (int)value; }
        }

        [ParseFieldName("categoryType")]
        public int CategoryTypeForParse
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
    }
}